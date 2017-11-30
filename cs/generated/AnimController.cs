using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "anim_controller")]
	public class AnimController : Component
	{
		public AnimController(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "anim_controller" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr scene, int cmp, string value);


		public string Source
		{
			get { return getSource(scene_, componentId_); }
			set { setSource(scene_, componentId_, value); }
		}

	} // class
} // namespace
