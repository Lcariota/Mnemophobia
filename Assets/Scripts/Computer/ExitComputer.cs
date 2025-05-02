using StudioProject.PlayerControl;
using UnityEngine;

public class ExitComputer : MonoBehaviour
{
    public GameObject computerObject; 
    public GameObject gameplayCanvas; 

    public void ExitInteraction()
    {
        if (computerObject != null)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null)
            {
                player.enabled = true;

                SkinnedMeshRenderer skinnedMesh = player.GetComponentInChildren<SkinnedMeshRenderer>();
                if (skinnedMesh != null)
                {
                    skinnedMesh.enabled = true;
                }

                // Reattach Main Camera to Player
                Camera mainCamera = Camera.main;
                if (mainCamera != null)
                {
                    mainCamera.transform.SetParent(player.transform); 
                    mainCamera.transform.localPosition = Vector3.zero; 
                    mainCamera.transform.localRotation = Quaternion.identity; 
                }
            }

            if (gameplayCanvas != null)
            {
                gameplayCanvas.SetActive(true);
            }

            Transform computerCanvasTransform = computerObject.transform.Find("ComputerCanvas");
            if (computerCanvasTransform != null)
            {
                computerCanvasTransform.gameObject.SetActive(false);
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}