using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class NavigationScene : IScene
	{
		public static string Type { get { return "navigation"; } }

		public NavigationScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(NavigationScene _value)
		{
			return _value.instance_;
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool load(IntPtr instance, string a0);

		public bool Load(string a0)
		{
			return load(instance_, a0);
		}

	}
}
