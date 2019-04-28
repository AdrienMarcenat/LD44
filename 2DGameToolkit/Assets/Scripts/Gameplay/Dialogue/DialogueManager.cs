using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour, IDialogueManager
    {
        [SerializeField] private Text m_DialogeText;
        [SerializeField] private Text m_NameText;
        [SerializeField] private Animator m_Animator;
        [SerializeField] private List<DialogueOptionButton> m_OptionButtons = new List<DialogueOptionButton>();

        private bool m_IsInDialogue = false;
        private bool m_IsWaitingForInput = false;
        private Node m_CurrentNode;
        private static string ms_DialogueDirectory = "/Dialogues/";

        private Dialogue m_Dialogue;

        void Awake()
        {
            DialogueManagerProxy.Open (this);
            this.RegisterAsListener ("Player", typeof (PlayerInputGameEvent));
            foreach (DialogueOptionButton optionButton in m_OptionButtons)
            {
                optionButton.Reset();
            }
        }

        void OnDestroy()
        {
            this.UnregisterAsListener ("Player");
            DialogueManagerProxy.Close (this);
        }

        private void StartDialogue ()
        {
            new GameFlowEvent (EGameFlowAction.StartDialogue).Push ();
            m_IsInDialogue = true;
            m_Animator.SetBool ("IsOpen", true);
            Assert.IsTrue(m_Dialogue != null, "Cannot start a null dialogue !");
            Assert.IsTrue(m_Dialogue.m_Nodes.Count != 0, "Cannot start an empty dialogue!");
            DisplayNode(m_Dialogue.GetRootNodeID());
        }

        public void DisplayNode (string nodeID)
        {
            if (nodeID == "")
            {
                EndDialogue ();
                return;
            }
            StopAllCoroutines ();
            m_CurrentNode = m_Dialogue.GetNode(nodeID);
            m_NameText.text = m_CurrentNode.m_Name;
            m_DialogeText.text = "";
            foreach(DialogueOptionButton optionButton in m_OptionButtons)
            {
                optionButton.Reset();
            }
            StartCoroutine (DisplayNodeRoutine(m_CurrentNode));
        }

        IEnumerator DisplayNodeRoutine(Node node)
        {
            m_DialogeText.text = "";
            foreach (char letter in node.m_Text.ToCharArray())
            {
                m_DialogeText.text += letter;
                yield return null;
            }
            if(node.m_Options.Count == 0)
            {
                StartCoroutine (WaitForInput ());
            }
            else
            {
                DisplayOptions (node.m_Options);
            }
        }
        IEnumerator WaitForInput ()
        {
            m_IsWaitingForInput = true;
            while (m_IsWaitingForInput)
            {
                yield return null;
            }
            DisplayNode (m_CurrentNode.m_NextNodeID);
        }

        private void DisplayOptions(List<Option> options)
        {
            Assert.IsTrue(options.Count <= m_OptionButtons.Count, "too many options to display");
            for(int i = 0; i < options.Count; i++)
            {
                m_OptionButtons[i].gameObject.SetActive(true);
                m_OptionButtons[i].SetOption(options[i]);
            }
        }

        public void EndDialogue()
        {
            StopAllCoroutines ();
            m_Animator.SetBool ("IsOpen", false);
            m_IsInDialogue = false;
            new GameFlowEvent (EGameFlowAction.EndDialogue).Push ();
            m_Dialogue = null;
        }

        public void TriggerDialogue(string tag)
        {
            Assert.IsFalse (m_IsInDialogue, "Already in dialogue, cannot start another one");
            string filename = Application.streamingAssetsPath + ms_DialogueDirectory + tag + ".xml";
            Dialogue dialogue = XMLSerializerHelper.Deserialize<Dialogue>(filename);
            m_Dialogue = dialogue;
            StartDialogue();
        }

        public void OnGameEvent (PlayerInputGameEvent inputEvent)
        {
            if (m_IsInDialogue && m_IsWaitingForInput)
            {
                string input = inputEvent.GetInput ();
                EInputState state = inputEvent.GetInputState ();
                if (input ==  "Enter" && state == EInputState.Down)
                {
                    m_IsWaitingForInput = false;
                }
            }
        }
    }
}