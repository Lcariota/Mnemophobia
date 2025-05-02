using UnityEngine;

[CreateAssetMenu(fileName = "InventoryInteraction", menuName = "Interaction/InventoryInteraction")]
public class InventoryInteraction : Interaction
{
    [SerializeField] private string itemTag;      // Internal logic
    [SerializeField] private string itemTitle;    // For UI display
    [SerializeField] private Sprite itemImage;    // Icon for UI display

    public float destroyDelay = 5f;

    public override void Interact(GameObject interactor, GameObject interactable)
    {
        var inventory = interactor.GetComponent<InventoryManager>();
        if (inventory != null)
        {
            // Add item to inventory
            inventory.AddItem(itemTag, itemTitle, itemImage);

            // Trigger event if the object has an ObjEventTrigger
            var trigger = interactable.GetComponent<ObjEventTrigger>();
            if (trigger != null)
                trigger.TriggerEvent();

            // Disable renderers and colliders instead of deactivating the whole object
            foreach (var renderer in interactable.GetComponentsInChildren<MeshRenderer>())
                renderer.enabled = false;

            foreach (var collider in interactable.GetComponentsInChildren<Collider>())
                collider.enabled = false;

            // Use DelayedDestroyer to destroy after delay
            var destroyer = interactable.GetComponent<DelayedDestroyer>();
            if (destroyer == null)
                destroyer = interactable.AddComponent<DelayedDestroyer>();

            destroyer.DestroyAfter(destroyDelay);
        }
    }

    public string GetItemTag() => itemTag;
    public string GetItemTitle() => itemTitle;
    public Sprite GetItemImage() => itemImage;
}