using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "point_light")]
	public class PointLight : Component
	{
		public PointLight(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "point_light" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getDiffuseColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDiffuseColor(IntPtr scene, int cmp, Vec3 value);


		public Vec3 DiffuseColor
		{
			get { return getDiffuseColor(scene_, componentId_); }
			set { setDiffuseColor(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getSpecularColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpecularColor(IntPtr scene, int cmp, Vec3 value);


		public Vec3 SpecularColor
		{
			get { return getSpecularColor(scene_, componentId_); }
			set { setSpecularColor(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDiffuseIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDiffuseIntensity(IntPtr scene, int cmp, float value);


		public float DiffuseIntensity
		{
			get { return getDiffuseIntensity(scene_, componentId_); }
			set { setDiffuseIntensity(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSpecularIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpecularIntensity(IntPtr scene, int cmp, float value);


		public float SpecularIntensity
		{
			get { return getSpecularIntensity(scene_, componentId_); }
			set { setSpecularIntensity(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFOV(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFOV(IntPtr scene, int cmp, float value);


		public float FOV
		{
			get { return getFOV(scene_, componentId_); }
			set { setFOV(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAttenuation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAttenuation(IntPtr scene, int cmp, float value);


		public float Attenuation
		{
			get { return getAttenuation(scene_, componentId_); }
			set { setAttenuation(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRange(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRange(IntPtr scene, int cmp, float value);


		public float Range
		{
			get { return getRange(scene_, componentId_); }
			set { setRange(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getCastShadows(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCastShadows(IntPtr scene, int cmp, bool value);


		public bool IsCastShadows
		{
			get { return getCastShadows(scene_, componentId_); }
			set { setCastShadows(scene_, componentId_, value); }
		}

	} // class
} // namespace
