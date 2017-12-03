using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Lumix
{
    

public abstract class Resource
{
    public System.IntPtr __Instance;
    public abstract string GetResourceType();

    [MethodImplAttribute(MethodImplOptions.InternalCall)]
    public extern static string getPath(IntPtr resource);

    public string GetPath()
    {
        return getPath(__Instance);
    }
}


public class PrefabResource : Resource
{
    public override string GetResourceType() { return "prefab"; }
}


}