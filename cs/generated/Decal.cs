using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "decal")]
	public class Decal : Component
	{
		public Decal(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "decal" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMaterial(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaterial(IntPtr scene, int cmp, string value);


		public string Material
		{
			get { return getMaterial(scene_, componentId_); }
			set { setMaterial(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setScale(IntPtr scene, int cmp, Vec3 value);


		public Vec3 Scale
		{
			get { return getScale(scene_, componentId_); }
			set { setScale(scene_, componentId_, value); }
		}

	} // class
} // namespace
