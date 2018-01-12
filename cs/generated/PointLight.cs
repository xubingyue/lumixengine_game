using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "point_light")]
	public class PointLight : Component
	{
		public PointLight(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "point_light" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getDiffuseColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDiffuseColor(IntPtr scene, int cmp, Vec3 value);


		public Vec3 DiffuseColor
		{
			get { return getDiffuseColor(scene_, entity_.entity_Id_); }
			set { setDiffuseColor(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getSpecularColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpecularColor(IntPtr scene, int cmp, Vec3 value);


		public Vec3 SpecularColor
		{
			get { return getSpecularColor(scene_, entity_.entity_Id_); }
			set { setSpecularColor(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDiffuseIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDiffuseIntensity(IntPtr scene, int cmp, float value);


		public float DiffuseIntensity
		{
			get { return getDiffuseIntensity(scene_, entity_.entity_Id_); }
			set { setDiffuseIntensity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSpecularIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpecularIntensity(IntPtr scene, int cmp, float value);


		public float SpecularIntensity
		{
			get { return getSpecularIntensity(scene_, entity_.entity_Id_); }
			set { setSpecularIntensity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFOV(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFOV(IntPtr scene, int cmp, float value);


		public float FOV
		{
			get { return getFOV(scene_, entity_.entity_Id_); }
			set { setFOV(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAttenuation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAttenuation(IntPtr scene, int cmp, float value);


		public float Attenuation
		{
			get { return getAttenuation(scene_, entity_.entity_Id_); }
			set { setAttenuation(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRange(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRange(IntPtr scene, int cmp, float value);


		public float Range
		{
			get { return getRange(scene_, entity_.entity_Id_); }
			set { setRange(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getCastShadows(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCastShadows(IntPtr scene, int cmp, bool value);


		public bool IsCastShadows
		{
			get { return getCastShadows(scene_, entity_.entity_Id_); }
			set { setCastShadows(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
