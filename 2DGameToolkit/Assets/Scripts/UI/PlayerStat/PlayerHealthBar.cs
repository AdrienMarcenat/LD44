using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IPlayerStatWatcher
{
    [SerializeField] GameObject m_HPPrefab;

    private Health m_Health;
    private List<Image> m_HPs = new List<Image>();

    private void OnEnable ()
    {
        PlayerManagerProxy.Get().RegiterStatChangeCallback(this);
        m_Health = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ();
        this.RegisterAsListener ("Player", typeof (DamageGameEvent));
        Draw(PlayerManagerProxy.Get().GetPlayerStat().m_HP);
    }

    private void Draw(int hpCount)
    {
        foreach (Image image in m_HPs)
        {
            Destroy(image.gameObject);
        }
        m_HPs.Clear();
        for (int i = 0; i < hpCount; i++)
        {
            GameObject hp = Instantiate(m_HPPrefab);
            hp.transform.SetParent(transform, false);
            hp.transform.position += new Vector3(i * 0.5f, 0, 0);
            m_HPs.Add(hp.GetComponent<Image>());
        }
        int currenthealth = m_Health.GetCurrentHealth();
        for (int i = 0; i < m_HPs.Count; i++)
        {
            if (i < currenthealth)
            {
                m_HPs[i].color = Color.red;
            }
            else
            {
                m_HPs[i].color = Color.white;
            }
        }
    }

    private void OnDisable ()
    {
        this.UnregisterAsListener ("Player");
        PlayerManagerProxy.Get().UnregiterStatChangeCallback(this);
    }

    public void OnGameEvent (DamageGameEvent damageEvent)
    {
        Draw(m_Health.GetMaxHealth());
    }

    public void OnStatChanged(PlayerStat stat)
    {
        Draw(m_Health.GetMaxHealth());
    }
}
