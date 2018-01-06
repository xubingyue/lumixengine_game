using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class GuiScene : IScene
	{
		public static string Type { get { return "gui"; } }

		public GuiScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(GuiScene _value)
		{
			return _value.instance_;
		}

	}
}
