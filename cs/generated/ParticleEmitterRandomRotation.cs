using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_random_rotation")]
	public class ParticleEmitterRandomRotation : Component
	{
		public ParticleEmitterRandomRotation(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "particle_emitter_random_rotation" )) { }


	} // class
} // namespace
