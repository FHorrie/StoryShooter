using UnityEngine;

namespace StoryShooter
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerLook : MonoBehaviour
    {
        private PlayerInputManager _playerInputManager = null;

        // Parent node to allow the user to look up and down while maintaining screen space weapon position
        [SerializeField]
        private GameObject _playerVision;

        [SerializeField]
        Vector2 _sens = new Vector2(0.32f, 0.32f);

        void Start()
        {
            _playerInputManager = GetComponent<PlayerInputManager>();
        }

        void Update()
        {
            HandleLook();
        }

        void HandleLook()
        {
            if (!_playerInputManager)
                return;
            
            Vector2 lookMovement = _playerInputManager.LookVector;
            lookMovement *= _sens;

            // Y based rotation
            ///////////////////////////////////////////////////////////

            Vector3 currentBodyRotation = transform.rotation.eulerAngles;
            currentBodyRotation.y += lookMovement.x; 

            transform.rotation = Quaternion.Euler(currentBodyRotation);

            // X based rotation
            ///////////////////////////////////////////////////////////
            if(_playerVision)
            {
                Vector3 currentVisionPan = _playerVision.transform.rotation.eulerAngles;

                if (currentVisionPan.x > 180)
                    currentVisionPan.x -= 360;


                // Negate due to 2D Y component down direction being positive, while we want it to be negative
                currentVisionPan.x -= lookMovement.y;

                // Clamp value to prevent reverse aim :)
                currentVisionPan.x = Mathf.Clamp(currentVisionPan.x, -89.9f, 89.9f);

                Debug.Log("Vision X comp: " + currentVisionPan.x);

                if (currentVisionPan.x < 0)
                    currentVisionPan.x += 360;

                _playerVision.transform.rotation = Quaternion.Euler(currentVisionPan);
            }
        }
    }
}