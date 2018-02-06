using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "environment_probe")]
	public class EnvironmentProbe : Component
	{
		public EnvironmentProbe(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "environment_probe" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabledReflection(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabledReflection(IntPtr scene, int cmp, bool value);


		public bool IsEnabledReflection
		{
			get { return getEnabledReflection(scene_, entity_.entity_Id_); }
			set { setEnabledReflection(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getOverrideGlobalSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOverrideGlobalSize(IntPtr scene, int cmp, bool value);


		public bool IsOverrideGlobalSize
		{
			get { return getOverrideGlobalSize(scene_, entity_.entity_Id_); }
			set { setOverrideGlobalSize(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getRadianceSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadianceSize(IntPtr scene, int cmp, int value);


		public int RadianceSize
		{
			get { return getRadianceSize(scene_, entity_.entity_Id_); }
			set { setRadianceSize(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getIrradianceSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIrradianceSize(IntPtr scene, int cmp, int value);


		public int IrradianceSize
		{
			get { return getIrradianceSize(scene_, entity_.entity_Id_); }
			set { setIrradianceSize(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
