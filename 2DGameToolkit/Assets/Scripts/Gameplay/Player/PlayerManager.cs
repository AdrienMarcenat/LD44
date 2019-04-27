using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : IPlayerManager
{
    private GameObject m_Player;
    private PlayerStat m_PlayerStat = new PlayerStat();
    private Health m_PlayerHealth;
    private List<IPlayerStatWatcher> m_StatChangeCallbacks = new List<IPlayerStatWatcher>();

    private const string m_PlayerStatSavePath = "/playerstat.xml";

    public void OnEngineStart()
    {
        this.RegisterAsListener("Game", typeof(LevelEvent));
        this.RegisterAsListener("Player", typeof(ChangePlayerStatGameEvent));
    }

    public void OnEngineStop()
    {
        this.UnregisterAsListener("Game");
        this.UnregisterAsListener("Player");
    }

    public void OnGameEvent(LevelEvent levelEvent)
    {
        if(levelEvent.GetLevelIndex() == 0)
        {
            return;
        }
        if(levelEvent.IsEntered())
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            m_PlayerStat = (PlayerStat)SaveManagerProxy.Get().GetSavedObject(typeof(PlayerStat), m_PlayerStatSavePath);
            m_PlayerHealth = m_Player.GetComponent<Health>();
            m_PlayerHealth.SetMaxHealth(m_PlayerStat.m_HP);
        }
        else
        {
            SaveManagerProxy.Get().SaveObject(m_PlayerStat, m_PlayerStatSavePath);
        }
    }

    public void OnGameEvent(ChangePlayerStatGameEvent changePlayerStatGameEvent)
    {
        changePlayerStatGameEvent.GetStatChange().ChangeStats(m_PlayerStat);
        m_PlayerHealth.SetMaxHealth(m_PlayerStat.m_HP);
        foreach (IPlayerStatWatcher cb in m_StatChangeCallbacks)
        {
            cb.OnStatChanged(m_PlayerStat);
        }
        LevelManagerProxy.Get().NextLevel();
    }

    public PlayerStat GetPlayerStat()
    {
        return m_PlayerStat;
    }

    public GameObject GetPlayer()
    {
        return m_Player;
    }

    public void RegiterStatChangeCallback(IPlayerStatWatcher cb)
    {
        m_StatChangeCallbacks.Add(cb);
    }

    public void UnregiterStatChangeCallback(IPlayerStatWatcher cb)
    {
        m_StatChangeCallbacks.Remove(cb);
    }
}
