using UnityEngine;

public class PhoneConvos : MonoBehaviour
{
    public AudioSource ringingAudio;
    public DialogueManager dialogueManager;
    public DialogueBlock phoneCallBlock;

    private bool phoneInUse = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && !phoneInUse)
        {
            StartPhoneCall();
        }
    }

    void StartPhoneCall()
    {
        phoneInUse = true;
        ringingAudio.Stop(); 
        dialogueManager.onDialogueEnd.AddListener(EndPhoneCall); // listen for when dialogue ends
        dialogueManager.StartDialogue(phoneCallBlock); // trigger phone conversation
    }

    void EndPhoneCall()
    {
        dialogueManager.onDialogueEnd.RemoveListener(EndPhoneCall);
        phoneInUse = false;
    }
}