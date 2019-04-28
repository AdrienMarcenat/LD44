
public class PlayerSwordSlash : Bullet
{
    public override int GetModifiedDamage()
    {
        return m_Damage + 2 * PlayerManagerProxy.Get().GetPlayerStat().m_Force;
    }
}
