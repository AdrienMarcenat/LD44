using UnityEngine;
using System.Collections;

public class EnemyPlayerAiming : EnemyAI
{
    private Transform m_Target;

    void Awake ()
    {
        m_Target = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    protected override void Fire ()
    {
        m_WeaponManager.Fire (0, m_Target.position - transform.position, m_SizeModifier);
    }
}

