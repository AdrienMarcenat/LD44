using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovingObject))]
public class EnemySeekerMoving : EnemySeeker
{
    // This enemy will not move closer to the target than this distance
    [SerializeField] float m_KeepDistance = 0f;
    [SerializeField] float m_WaitTimeAfterCollision = 1f;
    [SerializeField] float m_SlowSpeed = 1f;
    [SerializeField] bool m_XLocked = false;
    [SerializeField] bool m_YLocked = false;

    private MovingObject m_Body;

    public override void Seek(GameObject target)
    {
        base.Seek(target);
        m_Body = GetComponent<MovingObject>();
        this.RegisterToUpdate(EUpdatePass.AI);
        EnemyPlayerAiming enemyAI = gameObject.AddComponent<EnemyPlayerAiming>();
        enemyAI.SetFireParameters(2, 1, 1, 0);
    }

    protected override void OnGameOver(bool real)
    {
        base.OnGameOver(real);
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
        m_Sprite.flipX = horizontal < 0;
    }

    protected override void OnPlayerCollision()
    {
        base.OnPlayerCollision();
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