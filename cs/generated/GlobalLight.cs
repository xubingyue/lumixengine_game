using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "global_light")]
	public class GlobalLight : Component
	{
		public GlobalLight(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "global_light" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setColor(IntPtr scene, int cmp, Vec3 value);


		public Vec3 Color
		{
			get { return getColor(scene_, entity_.entity_Id_); }
			set { setColor(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIntensity(IntPtr scene, int cmp, float value);


		public float Intensity
		{
			get { return getIntensity(scene_, entity_.entity_Id_); }
			set { setIntensity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getIndirectIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIndirectIntensity(IntPtr scene, int cmp, float value);


		public float IndirectIntensity
		{
			get { return getIndirectIntensity(scene_, entity_.entity_Id_); }
			set { setIndirectIntensity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFogDensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogDensity(IntPtr scene, int cmp, float value);


		public float FogDensity
		{
			get { return getFogDensity(scene_, entity_.entity_Id_); }
			set { setFogDensity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFogBottom(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogBottom(IntPtr scene, int cmp, float value);


		public float FogBottom
		{
			get { return getFogBottom(scene_, entity_.entity_Id_); }
			set { setFogBottom(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFogHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogHeight(IntPtr scene, int cmp, float value);


		public float FogHeight
		{
			get { return getFogHeight(scene_, entity_.entity_Id_); }
			set { setFogHeight(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getFogColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogColor(IntPtr scene, int cmp, Vec3 value);


		public Vec3 FogColor
		{
			get { return getFogColor(scene_, entity_.entity_Id_); }
			set { setFogColor(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getShadowCascades(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setShadowCascades(IntPtr scene, int cmp, Vec4 value);


		public Vec4 ShadowCascades
		{
			get { return getShadowCascades(scene_, entity_.entity_Id_); }
			set { setShadowCascades(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
