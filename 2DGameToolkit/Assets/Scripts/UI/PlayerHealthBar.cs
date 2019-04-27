using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] GameObject m_HPPrefab;

    private Health m_Health;
    private Image m_Image;
    private List<Image> m_HPs = new List<Image>();

    private void OnEnable ()
    {
        m_Image = GetComponent<Image> ();
        m_Health = GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ();
        this.RegisterAsListener ("Player", typeof (DamageGameEvent));

        for(int i = 0; i < PlayerManagerProxy.Get().GetPlayerStat().m_HP; i++)
        {
            GameObject hp = Instantiate(m_HPPrefab);
            hp.transform.SetParent(transform, false);
            hp.transform.position += new Vector3(i * 0.5f, 0, 0);
            m_HPs.Add(hp.GetComponent<Image>());
        }
    }

    private void OnDisable ()
    {
        this.UnregisterAsListener ("Player");
    }

    public void OnGameEvent (DamageGameEvent damageEvent)
    {
        int currenthealth = m_Health.GetCurrentHealth();
        for (int i = 0; i < m_HPs.Count; i++)
        {
            if(i < currenthealth)
            {
                m_HPs[i].color = Color.red;
            }
            else
            {
                m_HPs[i].color = Color.white;
            }
        }
    }
}
