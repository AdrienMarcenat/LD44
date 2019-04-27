using UnityEngine;

public class PlayerManager : IPlayerManager
{
    private GameObject m_Player;
    private PlayerStat m_PlayerStat = new PlayerStat();

    private const string m_PlayerStatSavePath = "/playerstat.xml";

    public void OnEngineStart()
    {
        this.RegisterAsListener("Game", typeof(LevelEvent));
    }

    public void OnEngineStop()
    {
        this.UnregisterAsListener("Game");
    }

    public void OnGameEvent(LevelEvent levelEvent)
    {
        if(levelEvent.IsEntered())
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            m_PlayerStat = (PlayerStat)SaveManagerProxy.Get().GetSavedObject(typeof(PlayerStat), m_PlayerStatSavePath);
        }
        else
        {
            SaveManagerProxy.Get().SaveObject(m_PlayerStat, m_PlayerStatSavePath);
        }
    }

    public PlayerStat GetPlayerStat()
    {
        return m_PlayerStat;
    }

    public GameObject GetPlayer()
    {
        return m_Player;
    }
}
