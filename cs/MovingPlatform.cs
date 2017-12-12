namespace Lumix
{

public class MovingPlatform : Lumix.Component
{
    Vec3 _StartingPos;
    double _T;
    public Vec3 m_Dir = new Vec3(1, 0, 0);
    public void Update(float dt)
    {
        _T += dt;
        float s = (float)System.Math.Sin(_T);
        entity.Position = _StartingPos + m_Dir * s;
    }

    public void OnStartGame()
    {
        _T = 0;
        _StartingPos = entity.Position;
    }
}


}