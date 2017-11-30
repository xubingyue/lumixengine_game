using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "navmesh_agent")]
	public class NavmeshAgent : Component
	{
		public NavmeshAgent(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "navmesh_agent" )) { }


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
		extern static bool getUseRootMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUseRootMotion(IntPtr scene, int cmp, bool value);


		public bool IsUseRootMotion
		{
			get { return getUseRootMotion(scene_, componentId_); }
			set { setUseRootMotion(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getGetRootMotionFromAnimation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGetRootMotionFromAnimation(IntPtr scene, int cmp, bool value);


		public bool IsGetRootMotionFromAnimation
		{
			get { return getGetRootMotionFromAnimation(scene_, componentId_); }
			set { setGetRootMotionFromAnimation(scene_, componentId_, value); }
		}

	} // class
} // namespace
