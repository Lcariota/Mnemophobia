using UnityEngine;

[CreateAssetMenu(fileName = "Phone", menuName = "Interaction/Phone")]
public class Phone : Interaction
{
    public override void Interact(GameObject interactor, GameObject interactable)
    {
        PhoneCallController phoneController = interactable.GetComponent<PhoneCallController>();
        if (phoneController != null)
        {
            phoneController.TriggerPhoneCall();
        }
    }
}