using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "physical_controller")]
	public class PhysicalController : Component
	{
		public PhysicalController(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "physical_controller" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr scene, int cmp, int value);


		public int Layer
		{
			get { return getLayer(scene_, componentId_); }
			set { setLayer(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void moveController(IntPtr instance, int cmp, Vec3 a0);

		public void MoveController(Vec3 a0)
		{
			moveController(scene_, componentId_, a0);
		}

	} // class
} // namespace
