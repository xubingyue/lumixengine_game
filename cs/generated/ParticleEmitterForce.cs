using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_force")]
	public class ParticleEmitterForce : Component
	{
		public ParticleEmitterForce(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "particle_emitter_force" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getAcceleration(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAcceleration(IntPtr scene, int cmp, Vec3 value);


		public Vec3 Acceleration
		{
			get { return getAcceleration(scene_, entity_.entity_Id_); }
			set { setAcceleration(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
