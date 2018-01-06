using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class PhysicsScene : IScene
	{
		public static string Type { get { return "physics"; } }

		public PhysicsScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(PhysicsScene _value)
		{
			return _value.instance_;
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int raycast(IntPtr instance, Vec3 a0, Vec3 a1, int a2);

		public Entity Raycast(Vec3 a0, Vec3 a1, Entity a2)
		{
			var ret = raycast(instance_, a0, a1, a2.entity_Id_);
			return Universe.GetEntity(ret);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool raycastEx(IntPtr instance, Vec3 a0, Vec3 a1, float a2, ref RaycastHit a3, int a4);

		public bool RaycastEx(Vec3 a0, Vec3 a1, float a2, ref RaycastHit a3, Entity a4)
		{
			return raycastEx(instance_, a0, a1, a2, ref a3, a4.entity_Id_);
		}

	}
}
