using StudioProject.PlayerControl;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewComputerInteraction", menuName = "Interactions/Computer")]
public class Computer : Interaction
{
    public override void Interact(GameObject interactor, GameObject interactable)
    {
        PlayerController player = interactor.GetComponent<PlayerController>();
        if (player != null)
        {
            player.enabled = false; // Disable movement
            
            SkinnedMeshRenderer skinnedMesh = player.GetComponentInChildren<SkinnedMeshRenderer>();
            if (skinnedMesh != null)
            {
                skinnedMesh.enabled = false;
            }
        }

        Camera playerCamera = Camera.main;
        ComputerObject computer = interactable.GetComponent<ComputerObject>();
        if (computer != null)
        {
            playerCamera.transform.position = computer.cameraHolder.position;
            playerCamera.transform.rotation = computer.cameraHolder.rotation;

            // Find and enable computer canvas
            Transform computerCanvasTransform = computer.transform.Find("ComputerCanvas");
            if (computerCanvasTransform != null)
            {
                computerCanvasTransform.gameObject.SetActive(true);
            }

            // Disable Outline script if present
            Outline outlineScript = computer.GetComponent<Outline>();
            if (outlineScript != null)
            {
                outlineScript.enabled = false; // Force disable
            }

            // Find and disable gameplay canvas
            GameObject gameplayCanvas = GameObject.FindGameObjectWithTag("GameplayCanvas");
            if (gameplayCanvas != null)
            {
                gameplayCanvas.SetActive(false);
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            computer.StartCoroutine(DisableOutlineWithDelay(computer));
        }
    }

    private System.Collections.IEnumerator DisableOutlineWithDelay(ComputerObject computer)
    {
        yield return new WaitForSeconds(0.1f); 

        Outline outlineScript = computer.GetComponent<Outline>();
        if (outlineScript != null)
        {
            outlineScript.enabled = false;  
        }
    }
}