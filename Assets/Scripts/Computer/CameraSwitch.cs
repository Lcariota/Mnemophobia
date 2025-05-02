using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraSwitch : MonoBehaviour
{
    public GameObject initialCameraContainer;
    public GameObject secondaryCameraContainer;
    public Camera mainCamera;  // Reference to the Main Camera
    public Button switchButton;  // Reference to the UI Button
    public Outline targetOutline; 


    void Start()
    {
        if (switchButton == null)
        {
            Debug.LogError("switchButton is not assigned!");
            return;
        }

        EventTrigger trigger = switchButton.GetComponent<EventTrigger>();

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEntry.callback.AddListener((eventData) => StartSwitching());
        trigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };
        pointerUpEntry.callback.AddListener((eventData) => StopSwitching());
        trigger.triggers.Add(pointerUpEntry);
    }

    void StartSwitching()
    {
        SwitchCamera(true);
    }

    void StopSwitching()
    {
        SwitchCamera(false);
    }

    void SwitchCamera(bool isSecondaryActive)
    {
        if (isSecondaryActive)
        {
            mainCamera.transform.SetParent(secondaryCameraContainer.transform);
        }
        else
        {
            mainCamera.transform.SetParent(initialCameraContainer.transform);
            
            // Disable Outline when switching back
            if (targetOutline != null)
            {
                targetOutline.enabled = false;
            }
        }

        mainCamera.transform.localPosition = Vector3.zero;
        mainCamera.transform.localRotation = Quaternion.identity;
    }
}