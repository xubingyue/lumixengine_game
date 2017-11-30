using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "lua_script")]
	public class LuaScript : Component
	{
		public LuaScript(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "lua_script" )) { }


	} // class
} // namespace
