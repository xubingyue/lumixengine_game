using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_text")]
	public class GuiText : Component
	{
		public GuiText(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "gui_text" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getText(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setText(IntPtr scene, int cmp, string value);


		public string Text
		{
			get { return getText(scene_, componentId_); }
			set { setText(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getFontSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFontSize(IntPtr scene, int cmp, int value);


		public int FontSize
		{
			get { return getFontSize(scene_, componentId_); }
			set { setFontSize(scene_, componentId_, value); }
		}

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
