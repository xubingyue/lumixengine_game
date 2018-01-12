using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "environment_probe")]
	public class EnvironmentProbe : Component
	{
		public EnvironmentProbe(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "environment_probe" )) { }


	} // class
} // namespace
