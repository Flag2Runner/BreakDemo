using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SocketManager))]
[RequireComponent(typeof(InventoryComponent))]
[RequireComponent(typeof(HealthComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameplayWidget gameplayWidgetPrefab;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float bodyTurnSpeed = 10f;
    [SerializeField] private ViewCamera viewCameraPrefab;
    [SerializeField] private float animTurnLerpScale = 5f;
    private GameplayWidget _gameplayWidget;
    
    private CharacterController _characterController;
    private ViewCamera _viewCamera;
    private InventoryComponent _inventoryComponent;
     
    private Animator _animator;
    private float _animTurnSpeed;
    private Vector2 _moveInput;
    private Vector2 _aimInput;

    private static readonly int animFwdId = Animator.StringToHash("ForwardAmt");
    private static readonly int animRightId = Animator.StringToHash("RightAmt");
    private static readonly int animTurnId = Animator.StringToHash("TurnAmt");
    private static readonly int SwitchWeaponId = Animator.StringToHash("SwitchWeapon");
    private static readonly int FireId = Animator.StringToHash("Firing");

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _inventoryComponent = GetComponent<InventoryComponent>();
        _gameplayWidget = Instantiate(gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += MoveInputUpdated;
        _gameplayWidget.AimStick.OnInputUpdated += AimInputUpdated;
        _gameplayWidget.AimStick.OnInputClicked += SwitchWeapon;
        _viewCamera = Instantiate(viewCameraPrefab);
        _viewCamera.SetFollowParent(transform);
    }

    private void SwitchWeapon()
    {
        _animator.SetTrigger(SwitchWeaponId);
    }

    public void AttackPoint()
    {
        _inventoryComponent.FireCurrentActiveWeapon();
    }
    public void WeaponSwitchPoint()
    {
        _inventoryComponent.EquipNextWeapon();  
    }

    private void AimInputUpdated(Vector2 inputVal)
    { 
        _aimInput = inputVal;
        _animator.SetBool(FireId, _aimInput != Vector2.zero); 
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        _moveInput = inputVal;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = _viewCamera.InputToWorldDir(_moveInput);
        _characterController.Move( moveDir * (speed * Time.deltaTime));

        Vector3 aimDir = _viewCamera.InputToWorldDir(_aimInput);
        if (aimDir == Vector3.zero)
        {
            aimDir = moveDir;
            _viewCamera.AddYawInput(_moveInput.x);
        }

        float angleDelta = 0f;
        if (aimDir != Vector3.zero)
        {
            Vector3 prevDir = transform.forward;
            Quaternion goalRot = Quaternion.LookRotation(aimDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, Time.deltaTime * bodyTurnSpeed);
            angleDelta = Vector3.SignedAngle(transform.forward, prevDir, Vector3.up);
        }

        _animTurnSpeed = Mathf.Lerp(_animTurnSpeed, angleDelta/Time.deltaTime, Time.deltaTime * animTurnLerpScale);
        _animator.SetFloat(animTurnId, _animTurnSpeed);

        float animFwdAmt = Vector3.Dot(moveDir, transform.forward);
        float animRightAmt = Vector3.Dot(moveDir, transform.right);

        _animator.SetFloat(animFwdId, animFwdAmt);
        _animator.SetFloat(animRightId, animRightAmt);
    }
}
