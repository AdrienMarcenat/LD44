namespace Dialogue
{
    public interface IDialogueManager
    {
        void DisplayNode(string nodeID);
        void EndDialogue();
        void TriggerDialogue(string tag);
    }

    public class DialogueManagerProxy : UniqueProxy<IDialogueManager>
    {
    }
}
