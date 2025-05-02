using UnityEngine;
using UnityEngine.InputSystem;

namespace StudioProject.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] public PlayerInput playerInput;
        [SerializeField] private GameObject menuCanvas;
        [SerializeField] private GameObject gameplayCanvas;
        [SerializeField] private GameObject pauseCanvas;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }
        public bool Jump { get; private set; }
        public bool Crouch { get; private set; }
        public bool Interact { get; private set; }
        public bool Pray { get; private set; }
        public bool ToggleFlashlight { get; private set; }

        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _jumpAction;
        private InputAction _crouchAction;
        private InputAction _interactAction;
        private InputAction _prayAction;
        private InputAction _useBeerAction;
        private InputAction _useHolyWaterAction;
        private InputAction _toggleFlashlightAction;
        private InputAction _menuAction;
        private InputAction _settingsAction;

        private bool prayKeyReleased = true;
        private bool isMenuActive = false;
        private bool isPaused = false;

        private void Awake()
        {
            HideCursor();

            _currentMap = playerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction = _currentMap.FindAction("Run");
            _jumpAction = _currentMap.FindAction("Jump");
            _crouchAction = _currentMap.FindAction("Crouch");
            _interactAction = _currentMap.FindAction("Interact");
            _prayAction = _currentMap.FindAction("Pray");
            _useBeerAction = _currentMap.FindAction("UseBeer");
            _useHolyWaterAction = _currentMap.FindAction("UseHolyWater");
            _toggleFlashlightAction = _currentMap.FindAction("ToggleFlashlight");
            _menuAction = _currentMap.FindAction("Menu");
            _settingsAction = _currentMap.FindAction("Settings");

            _moveAction.performed += OnMove;
            _lookAction.performed += OnLook;
            _runAction.performed += OnRun;
            _jumpAction.performed += OnJump;
            _crouchAction.performed += OnCrouch;
            _interactAction.performed += OnInteract;
            _prayAction.performed += OnPray;
            _useBeerAction.performed += context => Collectable.UseItem(0);
            _useHolyWaterAction.performed += context => Collectable.UseItem(1);
            _toggleFlashlightAction.performed += OnToggleFlashlight;
            _menuAction.performed += OnMenuToggle;
            _settingsAction.performed += OnSettingsToggle;

            _moveAction.canceled += OnMove;
            _lookAction.canceled += OnLook;
            _runAction.canceled += OnRun;
            _jumpAction.canceled += OnJump;
            _crouchAction.canceled += OnCrouch;
            _interactAction.canceled += OnInteract;
            _prayAction.canceled += OnPray;
            _toggleFlashlightAction.canceled += context => ToggleFlashlight = false;
        }

        private void OnMenuToggle(InputAction.CallbackContext context)
        {
            isMenuActive = !isMenuActive;
            menuCanvas.SetActive(isMenuActive);
            gameplayCanvas.SetActive(!isMenuActive);

            if (isMenuActive)
            {
                ShowCursor();
                DisableMovement();
                DisableLook();
            }
            else
            {
                HideCursor();
                EnableMovement();
                EnableLook();
            }
        }

        private void OnToggleFlashlight(InputAction.CallbackContext context) => ToggleFlashlight = context.ReadValueAsButton();

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void OnMove(InputAction.CallbackContext context) => Move = context.ReadValue<Vector2>();
        private void OnLook(InputAction.CallbackContext context) => Look = context.ReadValue<Vector2>();
        private void OnRun(InputAction.CallbackContext context) => Run = context.ReadValueAsButton();
        private void OnJump(InputAction.CallbackContext context) => Jump = context.ReadValueAsButton();
        private void OnCrouch(InputAction.CallbackContext context) => Crouch = context.ReadValueAsButton();
        private void OnInteract(InputAction.CallbackContext context) => Interact = context.ReadValueAsButton();

        private void OnPray(InputAction.CallbackContext context)
        {
            if (context.performed && prayKeyReleased)
            {
                prayKeyReleased = false;
                Pray = !Pray; // Toggle prayer state
            }
            else if (context.canceled)
            {
                prayKeyReleased = true; // Reset on key release
            }
        }

        private void OnEnable()
        { 
            _currentMap.Enable();
            _settingsAction.Enable();
        }
        private void OnDisable() 
        { 
            _currentMap.Disable();
            _settingsAction.Enable();
            }

        public void DisableMovement()
        {
            _moveAction.Disable();
            Move = Vector2.zero;
        }

        public void EnableMovement()
        {
            _moveAction.Enable();
        }

        public void DisableLook()
        {
            _lookAction.Disable();
            Look = Vector2.zero;
        }

        public void EnableLook()
        {
            _lookAction.Enable();
        }

        private void OnSettingsToggle(InputAction.CallbackContext context)
        {
            isPaused = !isPaused;
            pauseCanvas.SetActive(isPaused);
            gameplayCanvas.SetActive(!isPaused);

            if (isPaused)
            {
                Time.timeScale = 0f; // Pause time
                ShowCursor();
                DisableMovement();
                DisableLook();
            }
            else
            {
                Time.timeScale = 1f; // Resume time
                HideCursor();
                EnableMovement();
                EnableLook();
            }
        }
            public void ForcePauseState(bool pause, bool calledFromButton = false)
            {
                isPaused = pause; // Update internal pause state
                pauseCanvas.SetActive(pause);
                gameplayCanvas.SetActive(!pause);

                if (pause)
                {
                    Time.timeScale = 0f;
                    ShowCursor();
                    DisableMovement();
                    DisableLook();
                }
                else
                {
                    Time.timeScale = 1f;
                    HideCursor();
                    EnableMovement();
                    EnableLook();

                    // Sync with toggle if closed from the button
                    if (calledFromButton)
                    {
                        isPaused = false; // Ensure toggle state matches
                    }
                }
            }
    }
}
