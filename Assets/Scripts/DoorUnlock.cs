using UnityEngine;
using FS_Atmo;

public class DoorUnlock : MonoBehaviour
{
    [SerializeField] private GameObject keyObject; // Assign the key GameObject in the inspector
    [SerializeField] private MonoBehaviour doorUnlockScript; // Assign the door unlock script in the inspector

    private void Start()
    {
        if (doorUnlockScript != null)
        {
            doorUnlockScript.enabled = false; // Ensure the door unlock script starts disabled
        }
    }

    private void Update()
    {
        if (keyObject == null) // Checks if the key has been destroyed
        {
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        if (doorUnlockScript != null)
        {
            doorUnlockScript.enabled = true;

            // Set the door's isLocked to false
            SimpleOpenClose doorScript = GetComponent<SimpleOpenClose>();
            if (doorScript != null)
            {
                doorScript.isLocked = false;
            }

            Debug.Log("The door is now unlocked!");
            Destroy(this); // Optional: Destroy this script once the door is unlocked
        }
    }
}
