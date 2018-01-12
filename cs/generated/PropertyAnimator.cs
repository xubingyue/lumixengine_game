using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "property_animator")]
	public class PropertyAnimator : Component
	{
		public PropertyAnimator(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "property_animator" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getAnimation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimation(IntPtr scene, int cmp, string value);


		public string Animation
		{
			get { return getAnimation(scene_, entity_.entity_Id_); }
			set { setAnimation(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
