
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class HeroMove : MonoBehaviour, ISavedProgress
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

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.WorldData.PositionOnLevel.Level = CurrentLevel();
        progress.WorldData.PositionOnLevel.Position = this.transform.position.AsVectorSeril();
        Debug.Log($"Current Level {CurrentLevel()}");
        Debug.Log($"Update progress X: {transform.position.x}; Y: {transform.position.y}; Z: {transform.position.z}");
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

    private void Warp(Vector3Serial to)
    {
        RbPlyer.isKinematic = true;
        Debug.Log("Warp");
        transform.position = to.AsUnityVector().AddY(RbPlyer.transform.localScale.y);
        RbPlyer.isKinematic = false;
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

    private static string CurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }


}
