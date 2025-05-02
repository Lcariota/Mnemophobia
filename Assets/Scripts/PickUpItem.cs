using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "PickUpInteraction", menuName = "Interactions/Pick Up Item")]
public class PickUpItem : Interaction
{
    private string filePath => Path.Combine(Application.dataPath, "Items.txt");
    private static GameObject heldItem = null; 
    private static Transform container; 

    // Position offsets 
    public float offsetX = 0.19f;
    public float offsetY = 1.253f;
    public float offsetZ = 0.468f;

    public override void Interact(GameObject interactor, GameObject interactable)
    {
        if (container == null)
        {
            container = interactor.transform.Find("ItemContainer");

            if (container == null)
            {
                Debug.LogError("ItemContainer not found under the player!");
                return;
            }
        }

        if (heldItem != null)
        {
            DropItem();
        }

        PickUp(interactable);
    }

    private void UpdateContainerPosition()
    {
        if (container != null)
        {
            container.localPosition = new Vector3(offsetX, offsetY, offsetZ);
            Debug.Log("Container Local Position Set To: " + container.localPosition);
        }
    }

    private void PickUp(GameObject item)
    {
        Debug.Log("Picked up: " + item.name);
        WriteItemToFile(item.name);
        
        item.transform.SetParent(container);
        item.transform.localPosition = Vector3.zero; // Ensure it stays centered in the container

        // Disable physics 
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        heldItem = item;
    }

    private void DropItem()
    {
        Debug.Log("Dropped: " + heldItem.name);
        
        heldItem.transform.SetParent(null);
        heldItem.transform.position = container.position + Vector3.forward; // Drop in front of player

        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        heldItem = null;
    }

        private void WriteItemToFile(string itemName)
    {
        try
        {
            File.WriteAllText(filePath, "Currently Holding: " + itemName + "\n");
            Debug.Log("Item saved: " + itemName);
        }
        catch (IOException ex)
        {
            Debug.LogError("Error writing to file: " + ex.Message);
        }
    }
}