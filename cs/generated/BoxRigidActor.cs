using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "box_rigid_actor")]
	public class BoxRigidActor : Component
	{
		public BoxRigidActor(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "box_rigid_actor" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr scene, int cmp, int value);


		public int Layer
		{
			get { return getLayer(scene_, entity_.entity_Id_); }
			set { setLayer(scene_, entity_.entity_Id_, value); }
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
		extern static Vec3 getSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSize(IntPtr scene, int cmp, Vec3 value);


		public Vec3 Size
		{
			get { return getSize(scene_, entity_.entity_Id_); }
			set { setSize(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void applyForceToActor(IntPtr instance, int cmp, Vec3 a0);

		public void ApplyForceToActor(Vec3 a0)
		{
			applyForceToActor(scene_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void applyImpulseToActor(IntPtr instance, int cmp, Vec3 a0);

		public void ApplyImpulseToActor(Vec3 a0)
		{
			applyImpulseToActor(scene_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getActorVelocity(IntPtr instance, int cmp);

		public Vec3 GetActorVelocity()
		{
			return getActorVelocity(scene_, entity_.entity_Id_);
		}

	} // class
} // namespace
