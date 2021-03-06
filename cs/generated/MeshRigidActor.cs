using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "mesh_rigid_actor")]
	public class MeshRigidActor : Component
	{
		public MeshRigidActor(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "mesh_rigid_actor" )) { }


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
		extern static string getSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr scene, int cmp, string value);


		public string Source
		{
			get { return getSource(scene_, entity_.entity_Id_); }
			set { setSource(scene_, entity_.entity_Id_, value); }
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
