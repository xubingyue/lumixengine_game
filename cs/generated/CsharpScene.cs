using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class CsharpScene : IScene
	{
		public static string Type { get { return "csharp"; } }

		public CsharpScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(CsharpScene _value)
		{
			return _value.instance_;
		}

	}
}
