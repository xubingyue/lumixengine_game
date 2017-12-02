using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
     [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RaycastHit
    {
        public Vec3 position;
        public Vec3 normal;
        public int entity_id;
    }
}
