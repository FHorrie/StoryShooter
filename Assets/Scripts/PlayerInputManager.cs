using UnityEngine;


namespace StoryShooter
{
    public class PlayerInputManager : MonoBehaviour
    {
        private IA_PlayerInput _playerInput = null;
        private IA_PlayerInput.OnFootActions _onFootActions;
        private PlayerLocomotion _playerLocomotion = null;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _playerInput = new IA_PlayerInput();
            _onFootActions = _playerInput.OnFoot;
        }

        // Update is called once per frame
        private void Update()
        {

        }

        private void OnEnable()
        {
            _onFootActions.Enable();
        }

        private void OnDisable()
        {
            _onFootActions.Disable();
        }
    }
}