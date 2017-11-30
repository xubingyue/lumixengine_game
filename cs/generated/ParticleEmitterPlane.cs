using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_plane")]
	public class ParticleEmitterPlane : Component
	{
		public ParticleEmitterPlane(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "particle_emitter_plane" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getBounce(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBounce(IntPtr scene, int cmp, float value);


		public float Bounce
		{
			get { return getBounce(scene_, componentId_); }
			set { setBounce(scene_, componentId_, value); }
		}

	} // class
} // namespace
