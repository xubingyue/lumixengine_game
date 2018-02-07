using System.Collections.Generic;
using Lumix;


public class Car : Component
{
	public Entity ColliderEntity;
	public Entity TimeUI;
	public Entity SpeedUI;
	public Entity Mesh;
	public Entity LeftWheelParticles;
	public Entity RightWheelParticles;
	public float Force = 5000;

	SphereRigidActor _Collider;
	GuiText _TimeText;
	GuiText _SpeedText;
	ParticleEmitter _LeftParticlesEmitter;
	ParticleEmitter _RightParticlesEmitter;
	Dictionary<uint, bool> _Keys = new Dictionary<uint, bool>();
	float _Yaw;
	float _Time = 0;

	public void Start()
	{
		_Time = 0;
		_Keys.Clear();
		_Collider = ColliderEntity.GetComponent<SphereRigidActor>();
		_TimeText = TimeUI.GetComponent<GuiText>();
		_SpeedText = SpeedUI.GetComponent<GuiText>();
		_LeftParticlesEmitter = LeftWheelParticles.GetComponent<ParticleEmitter>();
		_RightParticlesEmitter = RightWheelParticles.GetComponent<ParticleEmitter>();
	}

	private float getRoll(Vec3 dir, Vec3 vel)
	{
		var d = dir.Normalized;
		d = new Vec3(d.z, 0, -d.x);
		var v = vel.Normalized;
		float t = d.DotProduct(v);
		return -(Mathf.HalfPI - Mathf.ACos(t)) * vel.Length / 30;
	}


	public void Update(float time_delta)
	{
		Vec3 velocity = _Collider.GetActorVelocity();

		// ui
		_Time += time_delta;
		_TimeText.Text = string.Format("{0:F2}", _Time);

		_SpeedText.Text = string.Format("{0:F1}", velocity.Length);

		// car
		entity.Position = ColliderEntity.Position - new Vec3(0, 0.7f, 0);
		Vec3 drag = velocity * -8;
		_Collider.ApplyForceToActor(drag);
		Vec3 dir = new Vec3(Mathf.Sin(_Yaw), 0, Mathf.Cos(_Yaw));
		if (velocity.Length > 0.1)
		{
			_Collider.ApplyForceToActor(dir * velocity.Length * 4);
		}

		float roll_angle = getRoll(dir, velocity);
		var roll = new Quat(new Vec3(0, 0, 1), roll_angle);
		Mesh.Rotation = new Quat(new Vec3(0, 1, 0), _Yaw) * roll;
		Mesh.Position = entity.Position;
		float camera_yaw = _Yaw;
		if(velocity.Length > 0.01f) camera_yaw = Mathf.Atan2(velocity.x, velocity.z);
		entity.Rotation = new Quat(new Vec3(0, 1, 0), camera_yaw);

		if (IsKeyDown('d')) _Yaw -= time_delta;
		if (IsKeyDown('a')) _Yaw += time_delta;
		if (IsKeyDown('w'))
		{
			Vec3 force = new Vec3();
			force.x = Mathf.Sin(_Yaw) * time_delta * Force;
			force.z = Mathf.Cos(_Yaw) * time_delta * Force;
			_Collider.ApplyForceToActor(force);
		}

		// particles
		if(velocity.Length < 3)
		{
			_LeftParticlesEmitter.IsAutoemit = false;
			_RightParticlesEmitter.IsAutoemit = false;
		}
		else
		{
			_LeftParticlesEmitter.IsAutoemit = roll_angle < -0.1f;
			_RightParticlesEmitter.IsAutoemit = roll_angle > 0.1f;
		}
	}


	private bool IsKeyDown(uint keycode)
	{
		return _Keys.ContainsKey(keycode) && _Keys[keycode];
	}


	public void OnInput(InputEvent ev)
	{
		if (ev is KeyboardInputEvent)
		{
			var kev = ev as KeyboardInputEvent;
			_Keys[kev.key_id] = kev.is_down;
		}
	}
}
