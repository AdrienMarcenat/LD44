using System.Collections.Generic;
using UnityEngine;

public class DemonBattleWall : MonoBehaviour
{
    [SerializeField] List<GameObject> m_BattleWalls;

    private void Awake()
    {
        this.RegisterAsListener("Game", typeof(DemonFightGameEvent));
        foreach(GameObject g in m_BattleWalls)
        {
            g.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        this.UnregisterAsListener("Game");
    }

    public void OnGameEvent(DemonFightGameEvent fightEvent)
    {
        foreach (GameObject g in m_BattleWalls)
        {
            g.SetActive(true);
        }
    }
}
