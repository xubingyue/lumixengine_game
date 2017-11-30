using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "camera")]
	public class Camera : Component
	{
		public Camera(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "camera" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSlot(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSlot(IntPtr scene, int cmp, string value);


		public string Slot
		{
			get { return getSlot(scene_, componentId_); }
			set { setSlot(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getOrthographicSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOrthographicSize(IntPtr scene, int cmp, float value);


		public float OrthographicSize
		{
			get { return getOrthographicSize(scene_, componentId_); }
			set { setOrthographicSize(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getOrthographic(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOrthographic(IntPtr scene, int cmp, bool value);


		public bool IsOrthographic
		{
			get { return getOrthographic(scene_, componentId_); }
			set { setOrthographic(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFOV(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFOV(IntPtr scene, int cmp, float value);


		public float FOV
		{
			get { return getFOV(scene_, componentId_); }
			set { setFOV(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getNear(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setNear(IntPtr scene, int cmp, float value);


		public float Near
		{
			get { return getNear(scene_, componentId_); }
			set { setNear(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFar(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFar(IntPtr scene, int cmp, float value);


		public float Far
		{
			get { return getFar(scene_, componentId_); }
			set { setFar(scene_, componentId_, value); }
		}

	} // class
} // namespace
