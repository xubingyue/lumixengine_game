using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class AudioScene : IScene
	{
		public static string Type { get { return "audio"; } }

		public AudioScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(AudioScene _value)
		{
			return _value.instance_;
		}

	}
}
