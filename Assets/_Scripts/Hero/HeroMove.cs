using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class HeroMove : MonoBehaviour, ISavedProgress
{
    [SerializeField] private Rigidbody RbPlayer;

    private IInputService _inputService;
    private CharacterSettings _characterSettings;
    private Camera _camera;

    [SerializeField] private bool _isJumped = false;
    [SerializeField] private bool _isTriggered = false;

    private Vector3 _movementVector;

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

    private void OnEnable()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        CalculateMovement();
        HandleJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.WorldData.PositionOnLevel.Level = CurrentLevel();
        progress.WorldData.PositionOnLevel.Position = this.transform.position.AsVectorSeril();
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
        {
            Vector3Serial savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition != null)
            {
                Warp(savedPosition);
            }
        }
    }

    public void RestartRotation()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    private void Warp(Vector3Serial to)
    {
        RbPlayer.isKinematic = true;
        transform.position = to.AsUnityVector().AddY(RbPlayer.transform.localScale.y);
        RbPlayer.isKinematic = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!_isTriggered)
        {
            _isJumped = false;
            _isTriggered = true;
        }
    }

    private void CalculateMovement()
    {
        _movementVector = Vector3.zero;
        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            _movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            _movementVector.y = 0;
            _movementVector.Normalize();
        }
    }

    private void HandleJump()
    {
        if (!_isJumped && _inputService.IsJumpButtonUp())
        {
            RbPlayer.AddForce(Vector3.up * _characterSettings.JumpForce, ForceMode.Impulse);
            _isJumped = true;
            _isTriggered = false;
        }
    }

    private void ApplyMovement()
    {
        if (_movementVector.sqrMagnitude > Constants.Epsilon)
        {
            if (!_isJumped)
            {
                RbPlayer.AddForce(_movementVector * _characterSettings.MovementForceOnGround, ForceMode.Force);
            }
            else
            {
                RbPlayer.AddForce(_movementVector * _characterSettings.MovementForceOnAir, ForceMode.Force);
            }
        }
    }

    public static string CurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }


}
