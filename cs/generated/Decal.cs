using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "decal")]
	public class Decal : Component
	{
		public Decal(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "decal" )) { }


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
		extern static Vec3 getScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setScale(IntPtr scene, int cmp, Vec3 value);


		public Vec3 Scale
		{
			get { return getScale(scene_, entity_.entity_Id_); }
			set { setScale(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
