using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter")]
	public class ParticleEmitter : Component
	{
		public ParticleEmitter(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "particle_emitter" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getLife(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLife(IntPtr scene, int cmp, Vec2 value);


		public Vec2 Life
		{
			get { return getLife(scene_, entity_.entity_Id_); }
			set { setLife(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getInitialSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setInitialSize(IntPtr scene, int cmp, Vec2 value);


		public Vec2 InitialSize
		{
			get { return getInitialSize(scene_, entity_.entity_Id_); }
			set { setInitialSize(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getSpawnPeriod(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpawnPeriod(IntPtr scene, int cmp, Vec2 value);


		public Vec2 SpawnPeriod
		{
			get { return getSpawnPeriod(scene_, entity_.entity_Id_); }
			set { setSpawnPeriod(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getAutoemit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAutoemit(IntPtr scene, int cmp, bool value);


		public bool IsAutoemit
		{
			get { return getAutoemit(scene_, entity_.entity_Id_); }
			set { setAutoemit(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getLocalSpace(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLocalSpace(IntPtr scene, int cmp, bool value);


		public bool IsLocalSpace
		{
			get { return getLocalSpace(scene_, entity_.entity_Id_); }
			set { setLocalSpace(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMaterial(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaterial(IntPtr scene, int cmp, string value);


		public string Material
		{
			get { return getMaterial(scene_, entity_.entity_Id_); }
			set { setMaterial(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Int2 getSpawnCount(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpawnCount(IntPtr scene, int cmp, Int2 value);


		public Int2 SpawnCount
		{
			get { return getSpawnCount(scene_, entity_.entity_Id_); }
			set { setSpawnCount(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
