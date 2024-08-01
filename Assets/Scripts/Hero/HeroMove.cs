using UnityEngine;
using Zenject;

public class HeroMove : MonoBehaviour
{
    [SerializeField] private float MovementForce = 2;
    [SerializeField] private float JumpForce = 50;
    [SerializeField] private Rigidbody RbPlyer;

    [SerializeField] private IInputService _inputService;
    private Camera _camera;

    [SerializeField] private bool _isJumped = false;
    [SerializeField] private bool _isTriggered = false;


    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
        Debug.Log($"{_inputService}");
    }


    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 movementVector = Vector3.zero;
        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            movementVector = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y);
            movementVector.Normalize();
            RbPlyer.AddForce(movementVector * MovementForce, ForceMode.Force);
        }
        if (!_isJumped)
        {
            if (_inputService.IsJumpButtonUp())
            {
                RbPlyer.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                _isJumped = true;
                _isTriggered = false;

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isTriggered)
        {
            _isJumped = false;
            _isTriggered = true;
        }
    }
}
