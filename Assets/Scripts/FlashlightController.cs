using UnityEngine;
using StudioProject.Manager;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    private bool isOn = false;
    private InputManager inputManager;

    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();

        if (inputManager == null)
        {
            Debug.LogError("InputManager not found in scene!");
            enabled = false;
            return;
        }

        if (flashlight != null)
        {
            flashlight.enabled = false; // Ensure flashlight is off 
            isOn = false;
        }
        else
        {
            Debug.LogError("Flashlight Light component not assigned!");
        }

        enabled = false; 
    }

    private void OnEnable()
    {
        if (inputManager != null)
        {
            inputManager.playerInput.actions["ToggleFlashlight"].performed += ToggleFlashlight;
        }
    }

    private void OnDisable()
    {
        if (inputManager != null)
        {
            inputManager.playerInput.actions["ToggleFlashlight"].performed -= ToggleFlashlight;
        }
    }

    private void ToggleFlashlight(InputAction.CallbackContext context)
    {
        isOn = !isOn;
        flashlight.enabled = isOn;
        Debug.Log("Flashlight Toggled: " + isOn);
    }

    public void TurnOffFlashlight()
    {
        isOn = false;
        if (flashlight != null)
        {
            flashlight.enabled = false;
        }
    }
}