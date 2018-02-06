using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "navmesh_agent")]
	public class NavmeshAgent : Component
	{
		public NavmeshAgent(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "navmesh_agent" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadius(IntPtr scene, int cmp, float value);


		public float Radius
		{
			get { return getRadius(scene_, entity_.entity_Id_); }
			set { setRadius(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeight(IntPtr scene, int cmp, float value);


		public float Height
		{
			get { return getHeight(scene_, entity_.entity_Id_); }
			set { setHeight(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getUseRootMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUseRootMotion(IntPtr scene, int cmp, bool value);


		public bool IsUseRootMotion
		{
			get { return getUseRootMotion(scene_, entity_.entity_Id_); }
			set { setUseRootMotion(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getGetRootMotionFromAnimation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGetRootMotionFromAnimation(IntPtr scene, int cmp, bool value);


		public bool IsGetRootMotionFromAnimation
		{
			get { return getGetRootMotionFromAnimation(scene_, entity_.entity_Id_); }
			set { setGetRootMotionFromAnimation(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool navigate(IntPtr instance, int cmp, Vec3 a0, float a1, float a2);

		public bool Navigate(Vec3 a0, float a1, float a2)
		{
			return navigate(scene_, entity_.entity_Id_, a0, a1, a2);
		}

	} // class
} // namespace
