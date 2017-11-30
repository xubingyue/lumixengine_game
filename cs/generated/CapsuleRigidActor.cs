using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "capsule_rigid_actor")]
	public class CapsuleRigidActor : Component
	{
		public CapsuleRigidActor(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "capsule_rigid_actor" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadius(IntPtr scene, int cmp, float value);


		public float Radius
		{
			get { return getRadius(scene_, componentId_); }
			set { setRadius(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeight(IntPtr scene, int cmp, float value);


		public float Height
		{
			get { return getHeight(scene_, componentId_); }
			set { setHeight(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getTrigger(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTrigger(IntPtr scene, int cmp, bool value);


		public bool IsTrigger
		{
			get { return getTrigger(scene_, componentId_); }
			set { setTrigger(scene_, componentId_, value); }
		}

	} // class
} // namespace
