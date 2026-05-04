using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerFacade _playerFacade;
    [SerializeField] private PlayerMovementConfig _playerMovementConfig;
    [SerializeField] private GravitySource2D[] _gravitySources;
    [SerializeField] private KeyboardPlayerInput _keyboardPlayerInput;
    [SerializeField] private MobilePlayerInput _mobilePlayerInput;
    [SerializeField] private bool _isMobileInput;

    private NearestGravitySourceProvider _gravitySourceProvider;

    private void Awake()
    {
        _mobilePlayerInput.gameObject.SetActive(_isMobileInput);

        CreateService();
        InitPlayer();
    }

    private void CreateService()
    {
        _gravitySourceProvider = new(_gravitySources);
    }

    private void InitPlayer()
    {
        IPlayerInput input = _isMobileInput ? _mobilePlayerInput : _keyboardPlayerInput;
        _playerFacade.PlayerGravity.Init(_gravitySourceProvider, _playerMovementConfig);
        _playerFacade.PlayerMovement.Init(input, _playerMovementConfig, _gravitySourceProvider, _playerFacade.PlayerGravity);
    }
}
