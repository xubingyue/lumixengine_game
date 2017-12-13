namespace Lumix
{

public class Animal : Lumix.Component
{
    float _NextMoveCountdown = 0;
    NavmeshAgent _NavmeshAgent;
    System.Random _Random;

    public void OnStartGame()
    {
        _Random = new System.Random();
        _NavmeshAgent = GetComponent<NavmeshAgent>();
    }

    public void Update(float dt)
    {
        _NextMoveCountdown -= dt;
        if (_NextMoveCountdown < 0)
        {
            _NextMoveCountdown = 5.0f;
            Vec3 new_dest = entity.Position;
            new_dest += new Vec3(_Random.Next(-3, 3), 0, _Random.Next(-3, 3));
            _NavmeshAgent.Navigate(new_dest, 1.0f, 0.3f);
        }
    }
}

}