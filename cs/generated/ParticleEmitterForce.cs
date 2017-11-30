using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_force")]
	public class ParticleEmitterForce : Component
	{
		public ParticleEmitterForce(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "particle_emitter_force" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getAcceleration(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAcceleration(IntPtr scene, int cmp, Vec3 value);


		public Vec3 Acceleration
		{
			get { return getAcceleration(scene_, componentId_); }
			set { setAcceleration(scene_, componentId_, value); }
		}

	} // class
} // namespace
