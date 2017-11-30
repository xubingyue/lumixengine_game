using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "csharp_script")]
	public class CsharpScript : Component
	{
		public CsharpScript(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "csharp_script" )) { }


	} // class
} // namespace
