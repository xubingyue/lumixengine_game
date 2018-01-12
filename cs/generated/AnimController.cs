using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "anim_controller")]
	public class AnimController : Component
	{
		public AnimController(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "anim_controller" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr scene, int cmp, string value);


		public string Source
		{
			get { return getSource(scene_, entity_.entity_Id_); }
			set { setSource(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getDefaultSet(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDefaultSet(IntPtr scene, int cmp, int value);


		public int DefaultSet
		{
			get { return getDefaultSet(scene_, entity_.entity_Id_); }
			set { setDefaultSet(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
