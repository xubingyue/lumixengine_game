using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_size")]
	public class ParticleEmitterSize : Component
	{
		public ParticleEmitterSize(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "particle_emitter_size" )) { }


	} // class
} // namespace
