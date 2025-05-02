using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public MoveFootsteps footstepMover;
    public Canvas targetCanvas;

    private void Update()
    {
        if (targetCanvas.gameObject.activeSelf)
        {
            footstepMover.StartFootstepMovement();
        }
    }
}