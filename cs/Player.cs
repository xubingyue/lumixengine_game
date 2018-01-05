using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Lumix
{



public class Player : Lumix.Component
{
    private const int KEYCODE_SPACE = 32;

    public float m_Yaw = 0;
    public float m_Pitch = 0;
    public float m_RotationSpeed = 0.01f;
    public Entity m_CameraEntity;
    private PhysicalController m_PhysicalController;
    private Dictionary<uint, bool> m_IsKeyDown = new Dictionary<uint, bool>();
    private Dictionary<uint, bool> m_IsKeyPressed = new Dictionary<uint, bool>();
    private float m_Jump = 0;
    public PrefabResource m_HitParticlePrefab;

    public void OnInput(InputEvent v)
    {
        m_IsKeyPressed.Clear();
        if(v is KeyboardInputEvent)
        {
            var k = v as KeyboardInputEvent;
            m_IsKeyDown[k.key_id] = k.is_down;
            m_IsKeyPressed[k.key_id] = true;
        }
        else if(v is MouseAxisInputEvent)
        {
            var k = v as MouseAxisInputEvent;
            m_Yaw -= k.x * m_RotationSpeed;
            m_Pitch -= k.y * m_RotationSpeed;
        }
        else if(v is MouseButtonInputEvent)
        {
            var k = v as MouseButtonInputEvent;
            if (k.is_down) Shoot();
        }
    }


    public void Start()
    {
        m_IsKeyDown[KEYCODE_SPACE] = false;
        m_IsKeyDown['w'] = false;
        m_IsKeyDown['a'] = false;
        m_IsKeyDown['s'] = false;
        m_IsKeyDown['d'] = false;
        m_PhysicalController= GetComponent<PhysicalController>();
    }


    private void Shoot()
    {
        var scene = Universe.GetScene<PhysicsScene>();
        Vec3 origin = m_CameraEntity.Position;
        Vec3 dir = -m_CameraEntity.Direction;
        RaycastHit hit = new RaycastHit();
        
        scene.RaycastEx(origin, dir, 100, ref hit, entity);

        if (hit.EntityID != -1)
        {
            Universe.InstantiatePrefab(m_HitParticlePrefab, hit.Position, new Quat(0, 0, 0, 1), 1);
        }
    }


    private void Move(Vec3 dir_local)
    {
        Quat rotation = new Quat(new Vec3(0, 1, 0), m_Yaw);
        m_PhysicalController.MoveController(rotation.Rotate(dir_local));
    }


    private void DetectInteractable()
    {
        var scene = Universe.GetScene<PhysicsScene>();
        Vec3 origin = m_CameraEntity.Position;
        Vec3 dir = -m_CameraEntity.Direction;
        RaycastHit hit = new RaycastHit();
        
        scene.RaycastEx(origin, dir, 100, ref hit, entity);

        if (hit.EntityID != -1)
        {
            Entity e = Universe.GetEntity(hit.EntityID);

            Holdable holdable = e.GetComponent<Holdable>();
            if(holdable != null)
            {
                if(m_IsKeyPressed.ContainsKey('e'))
                {
                    Engine.logError("holdable");
                }
            }
        }
    }


    public void Update(float dt)
    {
        DetectInteractable();

        Quat rotation = new Quat(new Vec3(0, 1, 0), m_Yaw);
        entity.SetRotation(rotation);
        if (m_IsKeyDown[KEYCODE_SPACE] && m_Jump == 0) m_Jump = 1;
        if (m_IsKeyDown['w']) Move(new Vec3(0, 0, -0.1f));
        if (m_IsKeyDown['s']) Move(new Vec3(0, 0, 0.1f));
        if (m_IsKeyDown['a']) Move(new Vec3(-0.1f, 0, 0));
        if (m_IsKeyDown['d']) Move(new Vec3(0.1f, 0, 0));
        
        if(m_Jump > 0)
        {
            Move(new Vec3(0, m_Jump * 5 * dt, 0));
            m_Jump -= dt;
            if(m_Jump < 0) m_Jump = 0;
        }

        Quat pitch_quat = new Quat(new Vec3(1, 0, 0), m_Pitch);
        m_CameraEntity.SetLocalRotation(pitch_quat);
    }
}

}