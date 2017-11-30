using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "animable")]
	public class Animable : Component
	{
		public Animable(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "animable" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getAnimation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimation(IntPtr scene, int cmp, string value);


		public string Animation
		{
			get { return getAnimation(scene_, componentId_); }
			set { setAnimation(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getStartTime(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setStartTime(IntPtr scene, int cmp, float value);


		public float StartTime
		{
			get { return getStartTime(scene_, componentId_); }
			set { setStartTime(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTimeScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTimeScale(IntPtr scene, int cmp, float value);


		public float TimeScale
		{
			get { return getTimeScale(scene_, componentId_); }
			set { setTimeScale(scene_, componentId_, value); }
		}

	} // class
} // namespace
