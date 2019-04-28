using UnityEngine;

public class EnemySeeker : Enemy
{
    [SerializeField] bool m_IsSeeking = false;
    [SerializeField] AudioClip m_SeekingSound;

    protected Transform m_Target;

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
    }
}