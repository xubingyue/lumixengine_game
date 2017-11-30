using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "d6_joint")]
	public class D6Joint : Component
	{
		public D6Joint(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "d6_joint" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getAxisPosition(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAxisPosition(IntPtr scene, int cmp, Vec3 value);


		public Vec3 AxisPosition
		{
			get { return getAxisPosition(scene_, componentId_); }
			set { setAxisPosition(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getAxisDirection(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAxisDirection(IntPtr scene, int cmp, Vec3 value);


		public Vec3 AxisDirection
		{
			get { return getAxisDirection(scene_, componentId_); }
			set { setAxisDirection(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLinearLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLinearLimit(IntPtr scene, int cmp, float value);


		public float LinearLimit
		{
			get { return getLinearLimit(scene_, componentId_); }
			set { setLinearLimit(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getSwingLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSwingLimit(IntPtr scene, int cmp, Vec2 value);


		public Vec2 SwingLimit
		{
			get { return getSwingLimit(scene_, componentId_); }
			set { setSwingLimit(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getTwistLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTwistLimit(IntPtr scene, int cmp, Vec2 value);


		public Vec2 TwistLimit
		{
			get { return getTwistLimit(scene_, componentId_); }
			set { setTwistLimit(scene_, componentId_, value); }
		}

	} // class
} // namespace
