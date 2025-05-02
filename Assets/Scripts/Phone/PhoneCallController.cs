using UnityEngine;

public class PhoneCallController : MonoBehaviour
{
    public AudioSource ringingAudio;
    public DialogueManager dialogueManager;
    public DialogueBlock phoneCallBlock;

    public RingController phoneRingController;

    private bool phoneInUse = false;

     public void BeginPhoneSequence()
    {
        if (phoneInUse) return;

        if (phoneRingController != null)
        {
            phoneRingController.StartRinging();
        }

    }

    public void StartPhoneCallImmediately()
    {
        if (phoneInUse) return;

        if (phoneRingController != null)
        {
            phoneRingController.StopRinging(); 
        }

        if (dialogueManager != null && phoneCallBlock != null)
        {
            phoneInUse = true;
            dialogueManager.onDialogueEnd.AddListener(EndPhoneCall);
            dialogueManager.StartDialogue(phoneCallBlock);
        }
    }

    public void TriggerPhoneCall()
    {
        if (phoneInUse) return;

        phoneInUse = true;

        if (phoneRingController != null)
        {
            phoneRingController.StopRinging(); // Stop ringing once picked up
        }

        if (dialogueManager != null && phoneCallBlock != null)
        {
            dialogueManager.onDialogueEnd.AddListener(EndPhoneCall);
            dialogueManager.StartDialogue(phoneCallBlock);
        }
    }
    public void SetPhoneCallBlock(DialogueBlock newBlock)
    {
        phoneCallBlock = newBlock;
    }

    private void EndPhoneCall()
    {
        dialogueManager.onDialogueEnd.RemoveListener(EndPhoneCall);
        phoneInUse = false;
    }
}