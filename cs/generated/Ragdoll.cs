using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "ragdoll")]
	public class Ragdoll : Component
	{
		public Ragdoll(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "ragdoll" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr scene, int cmp, int value);


		public int Layer
		{
			get { return getLayer(scene_, entity_.entity_Id_); }
			set { setLayer(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
