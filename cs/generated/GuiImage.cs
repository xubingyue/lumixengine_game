using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_image")]
	public class GuiImage : Component
	{
		public GuiImage(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "gui_image" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setColor(IntPtr scene, int cmp, Vec4 value);


		public Vec4 Color
		{
			get { return getColor(scene_, componentId_); }
			set { setColor(scene_, componentId_, value); }
		}

	} // class
} // namespace
