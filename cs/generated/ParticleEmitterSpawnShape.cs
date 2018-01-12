using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_spawn_shape")]
	public class ParticleEmitterSpawnShape : Component
	{
		public ParticleEmitterSpawnShape(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "particle_emitter_spawn_shape" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadius(IntPtr scene, int cmp, float value);


		public float Radius
		{
			get { return getRadius(scene_, entity_.entity_Id_); }
			set { setRadius(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
