using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private CharacterController _characterController = null;
    private Vector3 _playerVelocity = Vector3.zero;
    
    [SerializeField]
    private float _movementSpeed = 5f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
    }

    //Perceive the inputs from input script and apply to character
    public void ProcessInput(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0 , input.y);
        _characterController.Move(transform.TransformDirection(moveDirection) * _movementSpeed * Time.deltaTime);
    }
}