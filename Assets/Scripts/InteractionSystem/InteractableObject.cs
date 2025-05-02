using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Interaction interaction; 

    public void TriggerInteraction(GameObject interactor)
    {
        if (interaction != null)
        {
            interaction.Interact(interactor, gameObject);
        }
    }
}