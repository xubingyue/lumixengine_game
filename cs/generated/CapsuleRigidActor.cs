using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "capsule_rigid_actor")]
	public class CapsuleRigidActor : Component
	{
		public CapsuleRigidActor(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "capsule_rigid_actor" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadius(IntPtr scene, int cmp, float value);


		public float Radius
		{
			get { return getRadius(scene_, entity_.entity_Id_); }
			set { setRadius(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeight(IntPtr scene, int cmp, float value);


		public float Height
		{
			get { return getHeight(scene_, entity_.entity_Id_); }
			set { setHeight(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getDynamic(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDynamic(IntPtr scene, int cmp, int value);


		public int Dynamic
		{
			get { return getDynamic(scene_, entity_.entity_Id_); }
			set { setDynamic(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getTrigger(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTrigger(IntPtr scene, int cmp, bool value);


		public bool IsTrigger
		{
			get { return getTrigger(scene_, entity_.entity_Id_); }
			set { setTrigger(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void applyForceToActor(IntPtr instance, int cmp, Vec3 a0);

		public void ApplyForceToActor(Vec3 a0)
		{
			applyForceToActor(scene_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getActorVelocity(IntPtr instance, int cmp);

		public Vec3 GetActorVelocity()
		{
			return getActorVelocity(scene_, entity_.entity_Id_);
		}

	} // class
} // namespace
