using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Inspect", menuName = "Interactions/Inspect")]
public class InspectObj : Interaction
{
    [TextArea]
    [SerializeField] private string inspectionText;

    public override void Interact(GameObject interactor, GameObject interactable)
    {
        InspectManagerUI uiManager = interactor.GetComponent<InspectManagerUI>();
        if (uiManager != null)
        {
            uiManager.ShowInspection(inspectionText);
        }
        else
        {
            Debug.LogWarning("No InspectManagerUI found on interactor.");
        }
    }
}