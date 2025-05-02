using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public DialogueBlock block;

    public void TriggerDialogue()
    {
        manager.StartDialogue(block);
    }
}