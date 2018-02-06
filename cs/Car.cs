using System.Collections.Generic;
using Lumix;


public class Car : Component
{
	public Entity ColliderEntity;
	SphereRigidActor _Collider;
	Dictionary<uint, bool> _Keys = new Dictionary<uint, bool>();
	float _Yaw;

	public void Start()
	{
		_Keys.Clear();
		_Collider = ColliderEntity.GetComponent<SphereRigidActor>();
	}

	public void Update(float time_delta)
	{
		entity.Position = ColliderEntity.Position - new Vec3(0, 0.7f, 0);
		Vec3 velocity = _Collider.GetActorVelocity();
		Vec3 drag = velocity * -8;
		_Collider.ApplyForceToActor(drag);
		if (velocity.Length > 0.1)
		{
			Vec3 dir = new Vec3(Mathf.Sin(_Yaw), 0, Mathf.Cos(_Yaw));
			_Collider.ApplyForceToActor(dir * velocity.Length * 4);
		}

		if (IsKeyDown('d')) _Yaw -= time_delta;
		if (IsKeyDown('a')) _Yaw += time_delta;
		if (IsKeyDown('w'))
		{
			Vec3 force = new Vec3();
			force.x = Mathf.Sin(_Yaw) * time_delta * 3000;
			force.z = Mathf.Cos(_Yaw) * time_delta * 3000;
			_Collider.ApplyForceToActor(force);
		}
		entity.Rotation = new Quat(new Vec3(0, 1, 0), _Yaw);
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
