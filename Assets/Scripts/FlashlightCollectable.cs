using UnityEngine;

[CreateAssetMenu(fileName = "Flashlight", menuName = "Interactions/Pick Up Flashlight")]
public class FlashlightCollectable : Interaction
{
    public override void Interact(GameObject interactor, GameObject interactable)
    {
        if (interactable.CompareTag("Flashlight") && interactable.layer == LayerMask.NameToLayer("Interactable"))
        {
            FlashlightController flashlightController = interactor.GetComponent<FlashlightController>();
            if (flashlightController == null)
            {
                Debug.LogError("FlashlightControl script not found on player!");
                return;
            }
            
            PickUp(interactable, flashlightController);
        }
    }

    private void PickUp(GameObject flashlight, FlashlightController flashlightControl)
    {
        Debug.Log("Picked up: " + flashlight.name);

        // Enable flashlight
        flashlightControl.enabled = true; 

        Destroy(flashlight);
    }
}