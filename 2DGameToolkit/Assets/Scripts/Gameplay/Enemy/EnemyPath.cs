using System.Collections;
using UnityEngine;

public class EnemyPath : Enemy
{
    [SerializeField] private BezierCurve m_Path;
    [SerializeField] private float m_Duration;
    [SerializeField] private bool m_Loop;
    [SerializeField] float m_WaitTimeAfterCollision = 1f;
    private int m_Direction = 1;

    private float m_Progress;
    private bool m_IsWaitingAfterCollision = false;

    protected new void Awake()
    {
        base.Awake();
        this.RegisterToUpdate(EUpdatePass.AI);
    }

    public void UpdateAI()
    {
        if(m_IsWaitingAfterCollision)
        {
            return;
        }
        m_Progress += m_Direction* (Time.deltaTime / m_Duration);
        if ((m_Direction == 1 && m_Progress > 1f) || (m_Direction == -1 && m_Progress < 0f))
        {
            if (m_Loop)
            {
                m_Direction *= -1;
            }
        }
        Vector2 position = m_Path.GetPoint (m_Progress);
        m_Sprite.flipX = transform.position.x - position.x < 0;
        transform.position = position;
    }

    protected override void OnGameOver()
    {
        base.OnGameOver();
        this.UnregisterToUpdate(EUpdatePass.AI);
    }

    public void SetPath (BezierCurve path)
    {
        m_Path = path;
    }

    public void SetDuration (float duration)
    {
        m_Duration = duration;
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

