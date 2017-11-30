using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "audio_listener")]
	public class AudioListener : Component
	{
		public AudioListener(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "audio_listener" )) { }


	} // class
} // namespace
