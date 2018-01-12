using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_linear_movement")]
	public class ParticleEmitterLinearMovement : Component
	{
		public ParticleEmitterLinearMovement(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "particle_emitter_linear_movement" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getX(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setX(IntPtr scene, int cmp, Vec2 value);


		public Vec2 X
		{
			get { return getX(scene_, entity_.entity_Id_); }
			set { setX(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getY(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setY(IntPtr scene, int cmp, Vec2 value);


		public Vec2 Y
		{
			get { return getY(scene_, entity_.entity_Id_); }
			set { setY(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getZ(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setZ(IntPtr scene, int cmp, Vec2 value);


		public Vec2 Z
		{
			get { return getZ(scene_, entity_.entity_Id_); }
			set { setZ(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
