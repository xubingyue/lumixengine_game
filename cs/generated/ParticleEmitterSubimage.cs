using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter_subimage")]
	public class ParticleEmitterSubimage : Component
	{
		public ParticleEmitterSubimage(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "particle_emitter_subimage" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getRows(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRows(IntPtr scene, int cmp, int value);


		public int Rows
		{
			get { return getRows(scene_, entity_.entity_Id_); }
			set { setRows(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getColumns(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setColumns(IntPtr scene, int cmp, int value);


		public int Columns
		{
			get { return getColumns(scene_, entity_.entity_Id_); }
			set { setColumns(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
