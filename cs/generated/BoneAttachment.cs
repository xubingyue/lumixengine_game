using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "bone_attachment")]
	public class BoneAttachment : Component
	{
		public BoneAttachment(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "bone_attachment" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getRelativePosition(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRelativePosition(IntPtr scene, int cmp, Vec3 value);


		public Vec3 RelativePosition
		{
			get { return getRelativePosition(scene_, componentId_); }
			set { setRelativePosition(scene_, componentId_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getRelativeRotation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRelativeRotation(IntPtr scene, int cmp, Vec3 value);


		public Vec3 RelativeRotation
		{
			get { return getRelativeRotation(scene_, componentId_); }
			set { setRelativeRotation(scene_, componentId_, value); }
		}

	} // class
} // namespace
