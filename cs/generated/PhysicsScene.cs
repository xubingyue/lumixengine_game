using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class PhysicsScene : IScene
	{
		public static string Type { get { return "physics"; } }

		public PhysicsScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(PhysicsScene _value)
		{
			return _value.instance_;
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity raycast(IntPtr instance, Vec3 a0, Vec3 a1, int a2);

		public Entity Raycast(Vec3 a0, Vec3 a1, Entity a2)
		{
			var ret = raycast(instance_, a0, a1, a2.entity_Id_);
			return Universe.GetEntity(ret);
		}

	}
}
