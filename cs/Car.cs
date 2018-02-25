using System.Collections.Generic;
using Lumix;


public class Car : Component
{
	public Entity FrontRaycastEntity;
	public Entity BackRaycastEntity;
	public Entity TimeUI;
	public Entity SpeedUI;
	public Entity Mesh;
	public Entity LeftWheelParticles;
	public Entity RightWheelParticles;
	public float Force = 20000;
	public float Mass = 1500;
	public float DragConstant = -500;

	GuiText _TimeText;
	GuiText _SpeedText;
	ParticleEmitter _LeftParticlesEmitter;
	ParticleEmitter _RightParticlesEmitter;
	Dictionary<uint, bool> _Keys = new Dictionary<uint, bool>();
	float _Yaw;
	float _Time;
	Vec3 _Velocity;

	public void Start()
	{
		_Time = 0;
		_Keys.Clear();
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
		// ui
		_Time += time_delta;
		_TimeText.Text = string.Format("{0:F2}", _Time);

		_SpeedText.Text = string.Format("{0:F1}", _Velocity.Length);

		// car
		entity.Position += _Velocity * time_delta;
		if(_Velocity.SquaredLength / time_delta > 0.01f)
		{
			Vec3 drag = _Velocity * DragConstant;
			Vec3 accel = drag / Mass;
			if ((accel * time_delta).SquaredLength > _Velocity.SquaredLength)
			{
				_Velocity = new Vec3(0, 0, 0);
			}
			else
			{
				_Velocity += accel * time_delta;
			}
		}
		else
		{
			_Velocity = new Vec3(0, 0, 0);
		}

		Vec3 dir = new Vec3(Mathf.Sin(_Yaw), 0, Mathf.Cos(_Yaw));
		if (_Velocity.Length > 0.1)
		{
			Vec3 dir_vel = dir * _Velocity.Length;
			float t = Mathf.Min(time_delta *5 , 1);
			_Velocity = _Velocity * (1 - t) + dir_vel * t;
		}

		float roll_angle = getRoll(dir, _Velocity);
		var roll = new Quat(new Vec3(0, 0, 1), roll_angle);
		Mesh.Rotation = new Quat(new Vec3(0, 1, 0), _Yaw) * roll;
		Mesh.Position = entity.Position;
		float camera_yaw = _Yaw;
		if(_Velocity.Length > 0.01f) camera_yaw = Mathf.Atan2(_Velocity.x, _Velocity.z);
		entity.Rotation = new Quat(new Vec3(0, 1, 0), camera_yaw);

		if (IsKeyDown('d')) _Yaw -= time_delta;
		if (IsKeyDown('a')) _Yaw += time_delta;
		if (IsKeyDown('w'))
		{
			Vec3 force = new Vec3();
			force.x = Mathf.Sin(_Yaw) * Force;
			force.z = Mathf.Cos(_Yaw) * Force;
			Vec3 accel = force / Mass;
			_Velocity += accel * time_delta;
		}

		// particles
		if(_Velocity.Length < 3)
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
