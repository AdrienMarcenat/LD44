using UnityEngine;
using System.Collections;

namespace Dialogue
{
    [RequireComponent (typeof (Collider2D))]
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private string m_DialogueTag;
        [SerializeField] private bool m_Repeat = false;
        private bool m_DoneOnce = false;

        private void OnTriggerEnter2D (Collider2D other)
        {
            if (other.tag == "Player" && (!m_DoneOnce || m_Repeat))
            {
                m_DoneOnce = true;
                DialogueManagerProxy.Get ().TriggerDialogue (m_DialogueTag);
            }
        }
    }
}
