using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovingObject))]
public class EnemySeeker : Enemy
{
    // This enemy will not move closer to the target than this distance
    [SerializeField] float m_KeepDistance = 0f;
    [SerializeField] float m_WaitTimeAfterCollision = 1f;
    [SerializeField] float m_SpeedMultiplierAfterCollision = 0.4f;
    [SerializeField] bool m_IsSeeking = false;
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

    private void OnDestroy()
    {
        this.UnregisterToUpdate(EUpdatePass.AI);
    }

    public void UpdateAI()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if ((m_Target.position - transform.position).magnitude > m_KeepDistance)
        {
            float horizontal = m_Target.transform.position.x - transform.position.x;
            float vertical = m_Target.transform.position.y - transform.position.y;

            m_Body.Move(horizontal, vertical);
        }
    }
    public override void OnPlayerCollision()
    {
        StartCoroutine(WaitAfterCollisionRoutine());
    }

    private IEnumerator WaitAfterCollisionRoutine()
    {
        m_Body.CancelVelocity();
        m_Body.ApplySpeedMultiplier(m_SpeedMultiplierAfterCollision);
        yield return new WaitForSecondsRealtime(m_WaitTimeAfterCollision);
        m_Body.ApplySpeedMultiplier(1 / m_SpeedMultiplierAfterCollision);
    }
}