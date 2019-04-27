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
        Debug.Log(m_Target.position - transform.position);
        m_WeaponManager.AddFireCommand (0, m_FireSalveNumber, m_SizeModifier, m_Target.position - transform.position);
    }
}

