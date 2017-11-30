using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_attractor")]
	public class ParticleEmitterAttractor : Component
	{
		public ParticleEmitterAttractor(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "particle_emitter_attractor" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getForce(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setForce(IntPtr scene, int cmp, float value);


		public float Force
		{
			get { return getForce(scene_, componentId_); }
			set { setForce(scene_, componentId_, value); }
		}

	} // class
} // namespace
