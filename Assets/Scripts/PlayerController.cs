using UnityEngine;
using System.Collections;
using StudioProject.Manager;

namespace StudioProject.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        // Camera
        [SerializeField] private float AinmBlendSpeed = 8.9f;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;

        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private float MouseSens = 21.9f;

        // Jumping
        [SerializeField, Range(10, 500)] private float JumpFactor = 260f;
        [SerializeField] private float Dis2Ground = 0.8f;
        [SerializeField] private LayerMask GroundCheck;
        [SerializeField] private float AirResistance = 0.8f;

        // Footstep Sounds
        [SerializeField] private AudioSource footstepSource;
        [SerializeField] private AudioClip walkClip;
        [SerializeField] private AudioClip sprintClip;
        [SerializeField] private float walkInterval = 0.5f;
        [SerializeField] private float sprintInterval = 0.3f;

        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private bool _hasAnimator;

        private int _xVelHash;
        private int _yVelHash;
        private int _zVelHash;

        private int _crouchHash;
        private int _jumpHash;
        private int _groundHash;
        private int _fallingHash;

        private bool _grounded;
        private float _xRotation;

        private const float _walkSpeed = 2f;
        private const float _runSpeed = 6f;

        private Vector2 _currentVelocity;
        private float _footstepTimer;

        void Start()
        {
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
            _zVelHash = Animator.StringToHash("Z_Velocity");

            _jumpHash = Animator.StringToHash("Jump");
            _fallingHash = Animator.StringToHash("Falling");
            _groundHash = Animator.StringToHash("Grounded");

            _crouchHash = Animator.StringToHash("Crouch");
        }

        private void FixedUpdate()
        {
            Move();
            SampleGround();
            HandleJump();
            HandleCrouch();
            HandleFootsteps();
            StepOffsetHandler();
        }

        private void LateUpdate()
        {
            CameraMovement();
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
            if (_inputManager.Crouch) targetSpeed = 1.5f;
            if (_inputManager.Move == Vector2.zero) targetSpeed = 0f;

            if (_grounded)
            {
                _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, AinmBlendSpeed * Time.fixedDeltaTime);
                _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, AinmBlendSpeed * Time.fixedDeltaTime);

                var xVelDifference = _currentVelocity.x - _playerRigidbody.linearVelocity.x;
                var zVelDifference = _currentVelocity.y - _playerRigidbody.linearVelocity.z;

                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);
            }
            else
            {
                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(_currentVelocity.x * AirResistance, 0, _currentVelocity.y * AirResistance)), ForceMode.VelocityChange);
            }

            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }

        private void CameraMovement()
        {
            if (!_hasAnimator) return;

            var Mouse_X = _inputManager.Look.x;
            var Mouse_Y = _inputManager.Look.y;
            Camera.position = CameraRoot.position;

            _xRotation -= Mouse_Y * MouseSens * Time.smoothDeltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0, Mouse_X * MouseSens * Time.smoothDeltaTime, 0));
        }

        private void HandleJump()
        {
            if (!_hasAnimator || !_inputManager.Jump) return;

            _animator.SetTrigger(_jumpHash);
            _playerRigidbody.AddForce(-_playerRigidbody.linearVelocity.y * Vector3.up, ForceMode.VelocityChange);
            _playerRigidbody.AddForce(Vector3.up * JumpFactor, ForceMode.Impulse);
            _animator.ResetTrigger(_jumpHash);
        }

        public void JumpAddForce()
        {
            _playerRigidbody.AddForce(-_playerRigidbody.linearVelocity.y * Vector3.up, ForceMode.VelocityChange);
            _playerRigidbody.AddForce(Vector3.up * JumpFactor, ForceMode.Impulse);
            _animator.ResetTrigger(_jumpHash);
        }

        private void SampleGround()
        {
            if (!_hasAnimator) return;

            RaycastHit hitInfo;
            if (Physics.Raycast(_playerRigidbody.worldCenterOfMass, Vector3.down, out hitInfo, Dis2Ground + 0.1f, GroundCheck))
            {
                _grounded = true;
                SetAnimationGrounding();
                return;
            }

            _grounded = false;
            _animator.SetFloat(_zVelHash, _playerRigidbody.linearVelocity.y);
            SetAnimationGrounding();
        }

        private void SetAnimationGrounding()
        {
            _animator.SetBool(_fallingHash, !_grounded);
            _animator.SetBool(_groundHash, _grounded);
        }

        private void HandleCrouch()
        {
            _animator.SetBool(_crouchHash, _inputManager.Crouch);
        }


        private void StepOffsetHandler()
        {
            if (!_grounded || _inputManager.Move == Vector2.zero) return;

            float stepHeight = 0.3f; 
            float stepCheckDistance = 0.5f; 

            RaycastHit hitLower;
            RaycastHit hitUpper;

            Vector3 forward = transform.forward * stepCheckDistance;
            Vector3 lowerRayStart = _playerRigidbody.position + Vector3.up * 0.1f;
            Vector3 upperRayStart = _playerRigidbody.position + Vector3.up * stepHeight;

            bool lowerHit = Physics.Raycast(lowerRayStart, forward, out hitLower, stepCheckDistance, GroundCheck);
            bool upperHit = Physics.Raycast(upperRayStart, forward, out hitUpper, stepCheckDistance, GroundCheck);

            Debug.DrawRay(lowerRayStart, forward, Color.red);
            Debug.DrawRay(upperRayStart, forward, Color.green);

            if (lowerHit && !upperHit)
            {
                _playerRigidbody.position += Vector3.up * stepHeight * Time.fixedDeltaTime * 5f;
            }
        }

        private void HandleFootsteps()
        {
            if (!_grounded || _inputManager.Move == Vector2.zero) return;

            _footstepTimer -= Time.deltaTime;

            if (_footstepTimer <= 0f)
            {
                if (_inputManager.Run)
                {
                    footstepSource.PlayOneShot(sprintClip);
                    _footstepTimer = sprintInterval;
                }
                else
                {
                    footstepSource.PlayOneShot(walkClip);
                    _footstepTimer = walkInterval;
                }
            }
        }
    }
}