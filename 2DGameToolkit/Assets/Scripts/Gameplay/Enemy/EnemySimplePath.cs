using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MovingObject))]
public class EnemySimplePath : Enemy
{
    [SerializeField] private Transform m_PathLeft;
    [SerializeField] private Transform m_PathRight;
    [SerializeField] float m_WaitTimeAfterCollision = 1f;
    [SerializeField] float m_Speed = 1f;

    private int m_Direction = 1;
    private MovingObject m_Body;
    private bool m_IsWaitingAfterCollision = false;

    protected new void Awake()
    {
        base.Awake();
        m_Body = GetComponent<MovingObject>();
        this.RegisterToUpdate(EUpdatePass.AI);
    }

    public void UpdateAI()
    {
        if (m_IsWaitingAfterCollision)
        {
            return;
        }
        if ((m_Direction == 1 && transform.position.x > m_PathRight.position.x) || (m_Direction == -1  && transform.position.x < m_PathLeft.position.x))
        {
            m_Direction *= -1;
        }
        m_Sprite.flipX = m_Direction < 0;
        m_Body.MoveHorizontal(m_Direction * m_Speed);
    }

    protected override void OnGameOver()
    {
        base.OnGameOver();
        this.UnregisterToUpdate(EUpdatePass.AI);
    }


    protected override void OnPlayerCollision()
    {
        base.OnPlayerCollision();
        m_Animator.SetTrigger("attack");
        StartCoroutine(WaitAfterCollisionRoutine());
    }

    private IEnumerator WaitAfterCollisionRoutine()
    {
        m_IsWaitingAfterCollision = true;
        yield return new WaitForSecondsRealtime(m_WaitTimeAfterCollision);
        m_IsWaitingAfterCollision = false;
    }
}


