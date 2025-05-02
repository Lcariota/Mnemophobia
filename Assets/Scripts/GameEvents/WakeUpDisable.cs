using UnityEngine;
using StudioProject.Manager;

public class WakeUpDisable : StateMachineBehaviour
{
    private InputManager inputManager;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (inputManager == null)
        {
            inputManager = Object.FindFirstObjectByType<InputManager>();
        }

        if (inputManager != null)
        {
            inputManager.DisableMovement();
            inputManager.DisableLook();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (inputManager == null)
        {
            inputManager = Object.FindFirstObjectByType<InputManager>();
        }

        if (inputManager != null)
        {
            inputManager.EnableMovement();
            inputManager.EnableLook();
        }
    }
}