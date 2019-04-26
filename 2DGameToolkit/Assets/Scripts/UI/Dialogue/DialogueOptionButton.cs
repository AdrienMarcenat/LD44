using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionButton : MonoBehaviour
{
    private Dialogue.Option m_Option;
    private Text m_Text;

    void Awake()
    {
        m_Text = GetComponentInChildren<Text>();
    }

    public void OnOptionChosen()
    {
        Dialogue.DialogueManagerProxy.Get().DisplayNode(m_Option.m_DestinationNodeID);
    }

    public void SetOption(Dialogue.Option option)
    {
        m_Option = option;
        m_Text.text = m_Option.m_Text;
        gameObject.SetActive (true);
    }

    public void Reset()
    {
        if (m_Option != null)
        {
            m_Option.m_Text = "";
            m_Option = null;
        }
        gameObject.SetActive (false);
    }
}
