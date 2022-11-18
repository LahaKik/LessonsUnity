using Assets.Code.Infrastrucrure;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController CharacterController;

    [Header("Main puposes:")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _airControl;

    [Header("Additional puposes:")]
    [SerializeField] private float _maxAcceleration;
    [SerializeField] private float _groundAcceleration;
    [SerializeField] private float _controlMovement;
    [SerializeField] private float _timeLerp;


    [Header("Debug puposes:")]
    [SerializeField] private Vector3 _accelerationVector;
    [SerializeField] private Vector3 _playerVelocity;
    [SerializeField] private float _acceleration;

    public bool _isGrounded;
    [SerializeField] private Vector3 _wishedDirection = Vector3.zero;
    private bool _wishhedJump;

    private InputService _inputService;
    private AllServices _services = AllServices.Instance;
    private const float Gravity = -9.81f;

    private void Start()
    {
        _inputService = _services.GetService<InputService>();
    }

    private void Update()
    {
        _isGrounded = CharacterController.isGrounded;
        _wishedDirection = GetInputVector();


        if(CharacterController.isGrounded == true)
        {
            _wishhedJump = _inputService.GetJumpButton();

            GroundMove(_wishedDirection);
            ApplyJump(_wishhedJump);
            Debug.DrawRay(transform.position, _accelerationVector * 5f, Color.yellow);
            Debug.DrawRay(transform.position, _playerVelocity, Color.red);

        }
        else
        {
            AirMove();
        }
        MovePlayer(_playerVelocity);
    }

    private void AirMove()
    {
        _playerVelocity.y += Gravity * Time.deltaTime;
    }

    private void GroundMove(Vector3 _wishedDirection)
    {
        if(_wishedDirection == Vector3.zero)
        {
            _acceleration = Mathf.Lerp(_acceleration, 0f, _timeLerp * Time.deltaTime);

            _playerVelocity *= _acceleration;
            return;
        }

        float frameAcceleration = _groundAcceleration * Time.deltaTime;
        if (Vector3.Angle(_accelerationVector, _wishedDirection) > 90f)
            frameAcceleration *= -1f;

        _acceleration = Mathf.Clamp(_acceleration + frameAcceleration, -_maxAcceleration, _maxAcceleration);

        _accelerationVector += (_wishedDirection * Mathf.Abs(frameAcceleration));
        _accelerationVector = Vector3.ClampMagnitude(_accelerationVector, _maxAcceleration);
        _playerVelocity = Vector3.Lerp(_playerVelocity, _wishedDirection * (1f + Mathf.Abs(_acceleration)),
            _controlMovement * Time.deltaTime);
    }

    private void ApplyJump(bool _wishhedJump)
    {
        if (_wishhedJump == false)
        {
            _playerVelocity.y = Gravity;
            return;
        }
        _playerVelocity.y = Mathf.Sqrt(-Gravity * _jumpForce);
    }

    private void MovePlayer(Vector3 movement) 
        => CharacterController.Move(movement * _speed * Time.deltaTime);

    private Vector3 GetInputVector()
        => _inputService.GetInputVector().normalized;


}
