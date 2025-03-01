using UnityEngine;
using UnityEngine.InputSystem;


namespace StoryShooter
{
    public struct Inputs
    {
        public InputAction movementAction;
        public InputAction lookAction;
        public InputAction jumpAction;
        public InputAction mainAction;
        public InputAction altAction;
    }

    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField]
        private InputActionAsset _inputAsset;

        //---------------------------------------------
        //[SerializeField]
        //private InputActionReference _movementAction;
        //---------------------------------------------
        // Alternative to allow user to hook up input with editor exposed vars

        private Inputs _playerInput;

        private Vector2 _movementVector = Vector2.zero;
        private Vector2 _lookVector = Vector2.zero;

        public Inputs PlayerInput
        {
            get { return _playerInput; }
        }

        public Vector2 MovementVector
        {
            get { return _movementVector; }
        }

        public Vector2 LookVector
        {
            get { return _lookVector; }
        }

        private void Awake()
        {
            if (_inputAsset == null)
                return;

            InputActionMap actionMap = _inputAsset.FindActionMap("OnFoot");
            _playerInput.movementAction = actionMap.FindAction("Movement");
            _playerInput.lookAction = actionMap.FindAction("Look");
            _playerInput.jumpAction = actionMap.FindAction("Jump");
            _playerInput.mainAction = actionMap.FindAction("Main");
            _playerInput.altAction = actionMap.FindAction("Alternate");

            _playerInput.movementAction.performed += MovementInput;
            _playerInput.movementAction.canceled += MovementInput;
        }

        private void Update()
        {
            LookInput();
        }

        private void MovementInput(InputAction.CallbackContext input)
        {
            _movementVector = _playerInput.movementAction.ReadValue<Vector2>();
            //Debug.Log("Movement: " + _movementVector.x + ", " + _movementVector.y);
        }

        private void LookInput()
        {
            _lookVector = _playerInput.lookAction.ReadValue<Vector2>();
            //Debug.Log("MouseDelta: " + _lookVector.x + ", " + _lookVector.y);
        }
    }
}