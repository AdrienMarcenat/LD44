using UnityEngine;
using System.Collections;

public class SwitchPanelButton : MonoBehaviour
{
    [SerializeField] GameObject m_Panel1;
    [SerializeField] GameObject m_Panel2;
    [SerializeField] bool m_DeactivateAtInit = false;

    public void OnEnable()
    {
        if(m_DeactivateAtInit)
        {
            m_Panel1.SetActive(false);
        }
    }

    public void SwitchPanel()
    {
        m_Panel1.SetActive(true);
        m_Panel2.SetActive(false);
    }
}
