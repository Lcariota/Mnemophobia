using StudioProject.PlayerControl;
using UnityEngine;
using System.Collections;
using StudioProject.Manager;
using UnityEngine.UI; 

public class PrayInteract : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private string requiredTag = "Pray";
    [SerializeField] private float interactDistance = 100f;
    [SerializeField] private SanityBar sanityBar;
    [SerializeField] private Image interactUI; // Reference to UI Image

    private bool isPraying = false;
    private InputManager inputManager;
    private Animator animator;
    private PlayerController playerController;
    private bool prayKeyPressed = false;
    private Outline currentOutline; 

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

        if (sanityBar == null)
        {
            sanityBar = FindFirstObjectByType<SanityBar>();
        }

        if (interactUI != null)
        {
            interactUI.gameObject.SetActive(false); // Ensure it's initially hidden
        }
    }

    private void Update()
    {
        HandleOutline(); 

        if (inputManager.Pray)
        {
            if (!prayKeyPressed)
            {
                prayKeyPressed = true;
                TogglePrayState();
            }
        }
        else
        {
            prayKeyPressed = false;
        }
    }

    private void HandleOutline()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance, interactableLayer))
        {
            if (hit.collider.CompareTag(requiredTag))
            {
                Outline outline = hit.collider.GetComponent<Outline>();

                if (outline != null)
                {
                    if (currentOutline != outline)
                    {
                        ClearOutline(); 
                        currentOutline = outline;
                        currentOutline.enabled = true; // Enable outline
                    }

                    if (interactUI != null)
                        interactUI.gameObject.SetActive(true); 
                    
                    return;
                }
            }
        }

        // If raycast no longer hits a valid object, remove the outline
        ClearOutline();
    }

    private void ClearOutline()
    {
        if (currentOutline != null)
        {
            currentOutline.enabled = false; 
            currentOutline = null;
        }

        if (interactUI != null)
            interactUI.gameObject.SetActive(false); 
    }

    private void TogglePrayState()
    {
        if (!isPraying && CanPray())
        {
            StartPraying();
        }
        else if (isPraying)
        {
            EndPraying();
        }
    }

    private bool CanPray()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance, interactableLayer))
        {
            return hit.collider.CompareTag(requiredTag);
        }
        return false;
    }

    private void StartPraying()
    {
        isPraying = true;
        inputManager.DisableMovement();
        inputManager.DisableLook();
        animator.SetBool("IsPraying", true);

        if (sanityBar != null)
        {
            sanityBar.StartPraying();
        }
    }

    private void EndPraying()
    {
        isPraying = false;
        animator.SetBool("IsPraying", false);
        inputManager.EnableMovement();
        inputManager.EnableLook();

        if (sanityBar != null)
        {
            sanityBar.StopPraying();
        }
    }
}