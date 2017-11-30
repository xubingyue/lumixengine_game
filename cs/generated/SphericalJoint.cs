using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "spherical_joint")]
	public class SphericalJoint : Component
	{
		public SphericalJoint(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "spherical_joint" )) { }


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
		extern static bool getUseLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUseLimit(IntPtr scene, int cmp, bool value);


		public bool IsUseLimit
		{
			get { return getUseLimit(scene_, componentId_); }
			set { setUseLimit(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLimit(IntPtr scene, int cmp, Vec2 value);


		public Vec2 Limit
		{
			get { return getLimit(scene_, componentId_); }
			set { setLimit(scene_, componentId_, value); }
		}

	} // class
} // namespace
