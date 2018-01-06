using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "renderable")]
	public class Renderable : Component
	{
		public Renderable(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "renderable" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabled(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabled(IntPtr scene, int cmp, bool value);


		public bool IsEnabled
		{
			get { return getEnabled(scene_, componentId_); }
			set { setEnabled(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr scene, int cmp, string value);


		public string Source
		{
			get { return getSource(scene_, componentId_); }
			set { setSource(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getKeepSkin(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setKeepSkin(IntPtr scene, int cmp, bool value);


		public bool IsKeepSkin
		{
			get { return getKeepSkin(scene_, componentId_); }
			set { setKeepSkin(scene_, componentId_, value); }
		}

	} // class
} // namespace
