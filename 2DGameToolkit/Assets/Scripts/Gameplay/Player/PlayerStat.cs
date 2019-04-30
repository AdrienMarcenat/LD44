
public class PlayerStat
{
    public int m_HP = 6;
    public int m_Force = 1;
    public int m_Magic = 1;
    public int m_JumpNumber = 1;
}

public interface IStatChange
{
    void ChangeStats(PlayerStat stat);
}

public class UpgradeForce : IStatChange
{
    public void ChangeStats(PlayerStat stat)
    {
        stat.m_Force++;
        stat.m_HP--;
    }
}
public class UpgradeMagic : IStatChange
{
    public void ChangeStats(PlayerStat stat)
    {
        stat.m_Magic++;
        stat.m_HP--;
    }
}
public class UpgradeJump : IStatChange
{
    public void ChangeStats(PlayerStat stat)
    {
        stat.m_JumpNumber++;
        stat.m_HP -= 2;
    }
}

public class ChangePlayerStatGameEvent : GameEvent
{
    public ChangePlayerStatGameEvent(IStatChange statChange) : base("Player")
    {
        m_StateChange = statChange;
    }

    public IStatChange GetStatChange()
    {
        return m_StateChange;
    }

    private IStatChange m_StateChange;
}
