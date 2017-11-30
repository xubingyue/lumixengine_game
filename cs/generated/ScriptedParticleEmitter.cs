using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "scripted_particle_emitter")]
	public class ScriptedParticleEmitter : Component
	{
		public ScriptedParticleEmitter(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, "scripted_particle_emitter" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMaterial(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaterial(IntPtr scene, int cmp, string value);


		public string Material
		{
			get { return getMaterial(scene_, componentId_); }
			set { setMaterial(scene_, componentId_, value); }
		}

	} // class
} // namespace
