namespace Lumix
{


public class AutoDestroy : Component
{
    public float time = 0;
    public void Update(float dt)
    {
        time -= dt;
        if(time < 0)
        {
            entity.Destroy();
        }
    } 
}


}