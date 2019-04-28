using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IPlayerStatWatcher
{
    [SerializeField] GameObject m_HPPrefab;
    [SerializeField] float m_Spacing = 1f;

    private Health m_Health;
    private List<Image> m_HPs = new List<Image>();

    private void OnEnable ()
    {
        PlayerManagerProxy.Get().RegiterStatChangeCallback(this);
        m_Health = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ();
        this.RegisterAsListener ("Player", typeof (DamageGameEvent));
        int hp = PlayerManagerProxy.Get().GetPlayerStat().m_HP;
        Draw(hp, hp);
    }

    private void Draw(int hpCount, int currenthealth)
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
            hp.transform.position += new Vector3(i * m_Spacing, 0, 0);
            m_HPs.Add(hp.GetComponent<Image>());
        }
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
        Draw(m_Health.GetMaxHealth(), m_Health.GetCurrentHealth());
    }

    public void OnStatChanged(PlayerStat stat)
    {
        Draw(m_Health.GetMaxHealth(), m_Health.GetCurrentHealth());
    }
}
