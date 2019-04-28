using UnityEngine;

[RequireComponent(typeof(MovingObject))]
[RequireComponent (typeof (WeaponManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 5;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;

    private MovingObject m_Mover;
    private WeaponManager m_WeaponManager;
    private Transform m_GroundCheck;
    private const float m_GroundedRadius = 0.2f;
    private bool m_Grounded;
    private Vector3 m_FacingDirection;
    private int m_JumpCount;

    private PlayerHSM m_PlayerHSM = new PlayerHSM();

    private static Vector3 m_Right = new Vector3 (1, 0, 0);
    private static Vector3 m_Left = new Vector3 (-1, 0, 0);

    void Awake ()
    {
        m_Mover = GetComponent<MovingObject> ();
        m_WeaponManager = GetComponent<WeaponManager> ();
        m_GroundCheck = transform.Find ("GroundCheck");
        m_FacingDirection = new Vector3 (1, 0, 0);
        this.RegisterAsListener ("Player", typeof(PlayerInputGameEvent));
        m_PlayerHSM.StartFlow();
    }

    private void OnDestroy ()
    {
        m_PlayerHSM.Stop();
        this.UnregisterAsListener ("Player");
    }

    private void FixedUpdate ()
    {
        bool grounded = false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll (m_GroundCheck.position, m_GroundedRadius, m_WhatIsGround);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                grounded = true;
                break;
            }
        }
        if(grounded != m_Grounded)
        {
            if (grounded)
            {
                m_JumpCount = 0;
            }
            m_Grounded = grounded;
        }
    }

    public void OnGameEvent(PlayerInputGameEvent inputEvent)
    {
        if(UpdaterProxy.Get().IsPaused())
        {
            return;
        }
        string input = inputEvent.GetInput ();
        EInputState state = inputEvent.GetInputState ();
        switch (input)
        {
            case "Jump":
                if (state == EInputState.Down)
                {
                    Jump ();
                }
                break;
            case "Right":
                Move (state == EInputState.Held ? 1 : 0);
                ChangeFacingDirection(m_Right);
                break;
            case "Left":
                Move (state == EInputState.Held ? -1 : 0);
                ChangeFacingDirection(m_Left);
                break;
            case "Blast":
                if (state == EInputState.Held)
                {
                    Fire ();
                }
                break;
            case "Slash":
                if (state == EInputState.Down)
                {
                    Slash();
                }
                break;
            default:
                break;
        }
    }

    private void ChangeFacingDirection(Vector3 newFacingDirection)
    {
        if (m_FacingDirection != newFacingDirection)
        {
            m_FacingDirection = newFacingDirection;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Jump()
    {
        if (m_Grounded || m_JumpCount < PlayerManagerProxy.Get().GetPlayerStat().m_JumpNumber)
        {
            m_JumpCount++;
            m_Mover.CancelYVelocity();
            m_Mover.ApplyForce (new Vector2 (0f, m_JumpForce));
        }
    }

    private void Move (float xDir)
    {
        if (m_Grounded || m_AirControl)
        {
            m_Mover.MoveHorizontal (xDir);
        }
    }

    private void Fire ()
    {
        m_WeaponManager.Fire ("Blast", m_FacingDirection);
    }

    private void Slash()
    {
        m_WeaponManager.Fire("Sword", m_FacingDirection);
    }
}
