using Assets.Code.Infrastrucrure;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController CharacterController;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Vector3 _verticalVelocity;
    private InputService _inputService;
    private AllServices _services = AllServices.Instance;


    private const float Gravity = -9.81f;

    private void Start()
    {
        _verticalVelocity = Vector3.up * Gravity;
        _inputService = _services.GetService<InputService>();
    }

    private void Update()
    {
        Vector3 inputVector = GetInputVector();
        ApplyGravity();
        ApplyJump();


        MovePlayer(inputVector * _speed);
        MovePlayer(_verticalVelocity * Time.deltaTime);
    }



    private void ApplyGravity()
    {
        if (CharacterController.isGrounded)
        {
            _verticalVelocity.y = 0f;
            Debug.Log("Grounded");

        }
        else
            _verticalVelocity.y += Gravity;
    }

    private void ApplyJump()
    {
        if (_inputService.GetJumpButton() && CharacterController.isGrounded)
            _verticalVelocity.y = _jumpForce;
    }

    private void MovePlayer(Vector3 movement) 
        => CharacterController.Move(movement * _speed * Time.deltaTime);

    private Vector3 GetInputVector()
        => _inputService.GetInputVector();


}
