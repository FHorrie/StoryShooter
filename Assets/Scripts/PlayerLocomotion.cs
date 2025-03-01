using StoryShooter;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StoryShooter
{
    [RequireComponent(typeof(PlayerInputManager))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerLocomotion : MonoBehaviour
    {
        const float GROUND_CHECK_DISTANCE = 0.2f;
        const string GROUND_MASK_NAME = "Ground";

        private PlayerInputManager _playerInputManager = null;
        private Rigidbody _rb = null;
        private Vector3 _playerVelocity = Vector3.zero;

        private bool _grounded = false;

        [SerializeField]
        private float _moveAcceleration = 10f;
        [SerializeField]
        private float _maxMoveSpeed = 10f;
        [SerializeField]
        private float _jumpForce = 15f;

        private void Start()
        {
            _playerInputManager = GetComponent<PlayerInputManager>();
            _rb = GetComponent<Rigidbody>();
            if (_playerInputManager == null || _rb == null)
                return;

            _rb.freezeRotation = true;
            _rb.maxLinearVelocity = _maxMoveSpeed;

            _playerInputManager.PlayerInput.jumpAction.performed += HandleJump;
        }

        private void FixedUpdate()
        {
            HandleMove();
            // Raycast for ground
            _grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down,
                GROUND_CHECK_DISTANCE, LayerMask.GetMask(GROUND_MASK_NAME));
        }

        private void HandleMove()
        {
            if (!_playerInputManager)
                return;

            Vector3 moveDir = transform.right * _playerInputManager.MovementVector.x + transform.forward * _playerInputManager.MovementVector.y;
            moveDir = moveDir.normalized * _moveAcceleration;
            _rb.AddForce(moveDir, ForceMode.Force);
        }

        private void HandleJump(InputAction.CallbackContext context)
        {
            if (_grounded)
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}