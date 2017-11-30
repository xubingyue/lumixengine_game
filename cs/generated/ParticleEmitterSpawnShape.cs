using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_spawn_shape")]
	public class ParticleEmitterSpawnShape : Component
	{
		public ParticleEmitterSpawnShape(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "particle_emitter_spawn_shape" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadius(IntPtr scene, int cmp, float value);


		public float Radius
		{
			get { return getRadius(scene_, componentId_); }
			set { setRadius(scene_, componentId_, value); }
		}

	} // class
} // namespace
