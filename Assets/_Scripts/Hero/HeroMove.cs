using UnityEngine;
using Zenject;

public class HeroMove : MonoBehaviour
{
    [SerializeField] private Rigidbody RbPlyer;

    private IInputService _inputService;
    private CharacterSettings _characterSettings;
    private Camera _camera;

    [SerializeField] private bool _isJumped = false;
    [SerializeField] private bool _isTriggered = false;



    [Inject]
    public void Construct(IInputService inputService, CharacterSettings characterSettings)
    {
        _inputService = inputService;
        _characterSettings = characterSettings;
    }


    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        MoveHandler();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!_isTriggered)
        {
            _isJumped = false;
            _isTriggered = true;
        }
    }

    private void MoveHandler()
    {
        Vector3 movementVector = Vector3.zero;
        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0;
            movementVector.Normalize();
            if (!_isJumped)
            {
                RbPlyer.AddForce(movementVector * _characterSettings.MovementForceOnGround, ForceMode.Force);
            }
            else if (_isJumped)
            {
                RbPlyer.AddForce(movementVector * _characterSettings.MovementForceOnAir, ForceMode.Force);
            }

        }
        if (!_isJumped)
        {
            if (_inputService.IsJumpButtonUp())
            {
                RbPlyer.AddForce(Vector3.up * _characterSettings.JumpForce, ForceMode.Impulse);
                _isJumped = true;
                _isTriggered = false;
            }
        }
    }

}
