using UnityEngine;

namespace Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    public class DemonDialogue : MonoBehaviour
    {
        [SerializeField] string m_DialogueTag;
        [SerializeField] GameObject m_Demon;
        private bool m_DoneOnce = false;

        private void Awake()
        {
            m_Demon.SetActive(false);
            this.RegisterAsListener("Game", typeof(GameFlowEvent));
        }

        private void OnDestroy()
        {
            this.UnregisterAsListener("Game");
        }

        public void OnGameEvent(GameFlowEvent gameFlowEvent)
        {
            if (gameFlowEvent.GetAction() == EGameFlowAction.EndDialogue)
            {
                m_Demon.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player" && !m_DoneOnce)
            {
                m_DoneOnce = true;
                m_Demon.SetActive(true);
                DialogueManagerProxy.Get().TriggerDialogue(m_DialogueTag);
            }
        }
    }
}
