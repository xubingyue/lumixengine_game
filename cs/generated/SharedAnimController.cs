using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "shared_anim_controller")]
	public class SharedAnimController : Component
	{
		public SharedAnimController(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "shared_anim_controller" )) { }


	} // class
} // namespace
