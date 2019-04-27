
public class PlayerMagicBar : PlayerStatBar
{
    protected override int GetPlayerStat(PlayerStat stat)
    {
        return stat.m_Magic;
    }
}