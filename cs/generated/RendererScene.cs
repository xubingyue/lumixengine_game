using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class RendererScene : IScene
	{
		public static string Type { get { return "renderer"; } }

		public RendererScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(RendererScene _value)
		{
			return _value.instance_;
		}

	}
}
