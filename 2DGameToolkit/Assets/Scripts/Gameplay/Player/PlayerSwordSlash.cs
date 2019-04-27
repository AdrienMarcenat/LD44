
public class PlayerSwordSlash : Bullet
{
    public override int GetModifiedDamage()
    {
        return m_Damage + PlayerManagerProxy.Get().GetPlayerStat().m_Force;
    }
}
