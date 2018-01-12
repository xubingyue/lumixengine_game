using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_alpha")]
	public class ParticleEmitterAlpha : Component
	{
		public ParticleEmitterAlpha(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "particle_emitter_alpha" )) { }


	} // class
} // namespace
