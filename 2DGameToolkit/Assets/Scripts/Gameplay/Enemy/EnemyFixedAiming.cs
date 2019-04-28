
public class EnemyFixedAiming : EnemyAI
{
    protected override void Fire ()
    {
        m_WeaponManager.Fire (0, m_ShootDirection.localPosition, m_SizeModifier);
    }
}

