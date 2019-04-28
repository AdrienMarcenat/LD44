using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStatBar : MonoBehaviour, IPlayerStatWatcher
{
    [SerializeField] GameObject m_StatPrefab;
    [SerializeField] float m_Spacing = 1f;
    private List<GameObject> m_Stats = new List<GameObject>();

    private void OnEnable()
    {
        PlayerManagerProxy.Get().RegiterStatChangeCallback(this);
        Draw(PlayerManagerProxy.Get().GetPlayerStat());
    }

    private void Draw(PlayerStat stat)
    {
        foreach(GameObject g in m_Stats)
        {
            Destroy(g);
        }
        m_Stats.Clear();
        for (int i = 0; i < GetPlayerStat(stat); i++)
        {
            GameObject g = Instantiate(m_StatPrefab);
            g.transform.SetParent(transform, false);
            g.transform.position += new Vector3(i * m_Spacing, 0, 0);
            m_Stats.Add(g);
        }
    }

    private void OnDisable()
    {
        PlayerManagerProxy.Get().UnregiterStatChangeCallback(this);
    }

    public void OnStatChanged(PlayerStat stat)
    {
        Draw(stat);
    }

    protected abstract int GetPlayerStat(PlayerStat stat);
}
