using UnityEngine;

public class EnemySeeker : Enemy
{
    [SerializeField] bool m_IsSeeking = false;
    [SerializeField] AudioClip m_SeekingSound;
    [SerializeField] SpriteRenderer m_HealthBar;
    private Collider2D m_Collider;

    protected Transform m_Target;

    protected new void Awake()
    {
        base.Awake();
        m_Collider = GetComponent<Collider2D>();
        m_HealthBar.enabled = false;
        m_Collider.enabled = false;
    }

    public bool IsSeeking()
    {
        return m_IsSeeking;
    }

    public virtual void Seek(GameObject target)
    {
        if (m_IsSeeking || IsDying())
            return;

        SoundManagerProxy.Get().PlayMultiple(m_SeekingSound);
        m_IsSeeking = true;
        m_Animator.SetTrigger("seeking");
        m_Target = target.transform;
        m_HealthBar.enabled = true;
        m_Collider.enabled = true;
    }
}