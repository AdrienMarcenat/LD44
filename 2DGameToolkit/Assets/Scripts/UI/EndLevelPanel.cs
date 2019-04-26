using UnityEngine;

public class EndLevelPanel : MonoBehaviour
{
    [SerializeField] GameObject m_EndLevelPanel;

    void Awake ()
    {
        m_EndLevelPanel.SetActive (false);
        this.RegisterAsListener("Level", typeof(EndLevelGameEvent));
    }

    void OnDestroy ()
    {
        this.UnregisterAsListener("Level");
    }

    void OnEvent (EndLevelGameEvent endLevelEvent)
    {
        m_EndLevelPanel.SetActive (true);
    }
}

