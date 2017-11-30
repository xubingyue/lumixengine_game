using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "distance_joint")]
	public class DistanceJoint : Component
	{
		public DistanceJoint(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "distance_joint" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDamping(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDamping(IntPtr scene, int cmp, float value);


		public float Damping
		{
			get { return getDamping(scene_, componentId_); }
			set { setDamping(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getStiffness(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setStiffness(IntPtr scene, int cmp, float value);


		public float Stiffness
		{
			get { return getStiffness(scene_, componentId_); }
			set { setStiffness(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTolerance(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTolerance(IntPtr scene, int cmp, float value);


		public float Tolerance
		{
			get { return getTolerance(scene_, componentId_); }
			set { setTolerance(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getLimits(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLimits(IntPtr scene, int cmp, Vec2 value);


		public Vec2 Limits
		{
			get { return getLimits(scene_, componentId_); }
			set { setLimits(scene_, componentId_, value); }
		}

	} // class
} // namespace
