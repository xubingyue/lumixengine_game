using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "terrain")]
	public class Terrain : Component
	{
		public Terrain(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "terrain" )) { }


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
		extern static float getXZScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setXZScale(IntPtr scene, int cmp, float value);


		public float XZScale
		{
			get { return getXZScale(scene_, componentId_); }
			set { setXZScale(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeightScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightScale(IntPtr scene, int cmp, float value);


		public float HeightScale
		{
			get { return getHeightScale(scene_, componentId_); }
			set { setHeightScale(scene_, componentId_, value); }
		}

	} // class
} // namespace
