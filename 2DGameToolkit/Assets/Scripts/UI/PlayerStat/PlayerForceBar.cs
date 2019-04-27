

public class PlayerForceBar : PlayerStatBar
{
    protected override int GetPlayerStat(PlayerStat stat)
    {
        return stat.m_Force;
    }
}

