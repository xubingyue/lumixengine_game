using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "ambient_sound")]
	public class AmbientSound : Component
	{
		public AmbientSound(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "ambient_sound" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool get3D(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void set3D(IntPtr scene, int cmp, bool value);


		public bool Is3D
		{
			get { return get3D(scene_, componentId_); }
			set { set3D(scene_, componentId_, value); }
		}

	} // class
} // namespace
