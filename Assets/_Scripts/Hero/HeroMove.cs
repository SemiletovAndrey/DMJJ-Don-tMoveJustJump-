using UnityEngine;
using Zenject;

public class HeroMove : MonoBehaviour, ISavedProgress
{
    [SerializeField] private Rigidbody RbPlayer;
    [SerializeField] private HeroAnimator HeroAnimator;

    private IInputService _inputService;
    private CharacterSettings _characterSettings;
    private Camera _camera;
    private IEventBus _eventBus;

    [SerializeField] private bool _isJumped = false;
    [SerializeField] private bool _isTriggered = false;

    private Vector3 _movementVector;

    [Inject]
    public void Construct(IInputService inputService, CharacterSettings characterSettings, IEventBus eventBus)
    {
        _inputService = inputService;
        _characterSettings = characterSettings;
        _eventBus = eventBus;
    }


    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _camera = Camera.main;
        _eventBus.Subscribe("OnStartDialogue", PlayerControllOff);
        _eventBus.Subscribe("OnEndDialogue", PlayerControllOn);
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

    private void OnDestroy()
    {
        _eventBus.Unsubscribe("OnStartDialogue", PlayerControllOff);
        _eventBus.Unsubscribe("OnEndDialogue", PlayerControllOn);
    }

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.WorldData.PositionOnLevel.Level = SceneStaticService.CurrentLevel();
        progress.WorldData.PositionOnLevel.Position = this.transform.position.AsVectorSeril();
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (SceneStaticService.CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
        {
            Debug.Log("Load prodress hero move");
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
    public void Warp(Vector3 to)
    {
        RbPlayer.isKinematic = true;
        transform.position = to + new Vector3(0, RbPlayer.transform.localScale.y,0);
        RbPlayer.isKinematic = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!_isTriggered)
        {
            if (_isJumped)
            {
                HeroAnimator.PlayLand();
            }
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
            HeroAnimator.PlayJump();
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

    public void PlayerControllOn()
    {
        this.enabled = true;
        Debug.Log("Player controller on");
    }
    public void PlayerControllOff()
    {
        this.enabled = false;
    }
}
