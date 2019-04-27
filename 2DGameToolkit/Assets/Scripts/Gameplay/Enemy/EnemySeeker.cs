using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovingObject))]
public class EnemySeeker : Enemy
{
    // This enemy will not move closer to the target than this distance
    [SerializeField] float m_KeepDistance= 0f;
    [SerializeField] float m_WaitTimeAfterCollision = 1f;
    [SerializeField] float m_SlowSpeed = 1f;
    [SerializeField] bool m_IsSeeking = false;
    [SerializeField] bool m_XLocked = false;
    [SerializeField] bool m_YLocked = false;
    [SerializeField] AudioClip m_SeekingSound;

    private MovingObject m_Body;
    private Transform m_Target;

    public bool IsSeeking()
    {
        return m_IsSeeking;
    }

    public virtual void Seek(GameObject target)
    {
        if (m_IsSeeking)
            return;

        SoundManagerProxy.Get().PlayMultiple(m_SeekingSound);
        m_IsSeeking = true;
        //m_Animator.SetTrigger("isSeeking");

        m_Target = target.transform;
        m_Body = GetComponent<MovingObject>();
        this.RegisterToUpdate(EUpdatePass.AI);
        EnemyPlayerAiming enemyAI = gameObject.AddComponent<EnemyPlayerAiming>();
        enemyAI.SetFireParameters(0, 1, 1, 1);
    }

    protected new void OnDestroy()
    {
        base.OnDestroy();
        this.UnregisterToUpdate(EUpdatePass.AI);
    }

    public void UpdateAI()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        float horizontal = m_XLocked ? 0 : m_Target.transform.position.x - transform.position.x;
        float vertical = m_YLocked ? 0 : m_Target.transform.position.y - transform.position.y;

        if ((m_Target.position - transform.position).magnitude > m_KeepDistance)
        {
            m_Body.MoveAccelerate(horizontal, vertical, Time.deltaTime);
        }
        else
        {
            m_Body.MoveDecelerate(horizontal, vertical, 5 * Time.deltaTime);
        }
    }

    public override void OnPlayerCollision()
    {
        StartCoroutine(WaitAfterCollisionRoutine());
    }

    private IEnumerator WaitAfterCollisionRoutine()
    {
        m_Body.CancelVelocity();
        m_Body.SetSmoothSpeed(m_SlowSpeed);
        yield return new WaitForSecondsRealtime(m_WaitTimeAfterCollision);
        m_Body.ResetSmoothSpeed();
    }
}