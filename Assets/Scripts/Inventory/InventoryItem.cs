using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Interaction/InventoryItem")]
public class InventoryItem : Interaction
{
    [SerializeField] private string itemTag;      // Internal logic
    [SerializeField] private string itemTitle;    // For UI display
    [SerializeField] private Sprite itemImage;    // Icon for UI display

    public override void Interact(GameObject interactor, GameObject interactable)
    {
        var inventory = interactor.GetComponent<InventoryManager>();
        if (inventory != null)
        {
            inventory.AddItem(itemTag, itemTitle, itemImage);
            Destroy(interactable); 
        }
    }

    public string GetItemTag() => itemTag;
    public string GetItemTitle() => itemTitle;
    public Sprite GetItemImage() => itemImage;
}