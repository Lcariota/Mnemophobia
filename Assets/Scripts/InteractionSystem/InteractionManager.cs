using UnityEngine;
using UnityEngine.UI;
using StudioProject.Manager;

namespace StudioProject.Interact
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private float interactDistance = 3f;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private Image interactUI; 

        private InputManager _inputManager;
        private InteractableObject _currentInteractable;
        private Outline _currentOutline;

        private void Start()
        {
            _inputManager = GetComponent<InputManager>();
            if (interactUI != null)
                interactUI.gameObject.SetActive(false); 
        }

        private void Update()
        {
            HandleHighlight();

            if (_inputManager.Interact && _currentInteractable != null)
            {
                _currentInteractable.TriggerInteraction(gameObject);
            }
        }

        private void HandleHighlight()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactDistance, interactableLayer))
            {
                InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

                if (interactable != null)
                {
                    // Check if there's something blocking the view
                    if (!IsObjectVisible(interactable.gameObject))
                    {
                        ResetOutline();
                        return;
                    }

                    if (_currentInteractable != interactable)
                    {
                        ResetOutline();
                        _currentInteractable = interactable;
                        _currentOutline = interactable.GetComponent<Outline>();

                        if (_currentOutline != null)
                        {
                            _currentOutline.enabled = true;
                        }

                        if (interactUI != null)
                            interactUI.gameObject.SetActive(true);
                    }
                    return;
                }
            }
            ResetOutline();
        }

        private void ResetOutline()
        {
            if (_currentOutline != null)
            {
                _currentOutline.enabled = false;
                _currentOutline = null;
            }
            _currentInteractable = null;

            if (interactUI != null)
                interactUI.gameObject.SetActive(false); 
        }

        private bool IsObjectVisible(GameObject obj)
        {
            Vector3 direction = obj.transform.position - Camera.main.transform.position;
            if (Physics.Raycast(Camera.main.transform.position, direction, out RaycastHit hit, interactDistance))
            {
                return hit.collider.gameObject == obj; // Object is visible only if it's the first thing hit
            }
            return false;
        }
    }
}