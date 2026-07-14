using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerInputHandler input;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private Animator animator;

    private CharacterController characterController;
    private Player player;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int DieHash = Animator.StringToHash("Die");

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GetComponent<Player>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        player.OnDead += OnPlayerDead;
    }

    private void OnDestroy()
    {
        if (player != null)
            player.OnDead -= OnPlayerDead;
    }

    private void Update()
    {
        if (player.IsDead)
            return;

        HandleMovement();
        HandleRotation();
        HandleWeapon();
        UpdateAnimation();
    }

    #region Movement

    private void HandleMovement()
    {
        Vector2 moveInput = input.MoveInput;

        float gravity = characterController.isGrounded ? -0.5f : -9.81f;
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        characterController.Move(
            new Vector3(moveInput.x * playerData.moveSpeed, gravity, moveInput.y * playerData.moveSpeed) * Time.deltaTime);
        //characterController.Move(
        //    moveDirection * playerData.moveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector2 moveInput = input.MoveInput;

        if (moveInput.sqrMagnitude < 0.01f)
            return;

        Vector3 lookDirection = new Vector3(moveInput.x, 0f, moveInput.y);

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            playerData.rotationSpeed * Time.deltaTime);
    }

    #endregion

    #region Weapon


    private void HandleWeapon()
    {
        if (input.IsFireHeld)
        {
            weaponController.Fire();
        }


        if (input.ReloadPressed)
        {
            weaponController.Reload();
            input.UseReload();
        }


        if (input.SwitchPressed)
        {
            weaponController.SwitchWeapon();
            input.UseSwitch();
        }


        if (input.BombPressed)
        {
            weaponController.ThrowGrenade();
            input.UseBomb();
        }
    }

    #endregion

    #region Animation

    private void UpdateAnimation()
    {
        float currentSpeed = input.MoveInput.magnitude * playerData.moveSpeed;
        animator.SetFloat(SpeedHash, currentSpeed);
    }

    private void OnPlayerDead()
    {
        characterController.enabled = false;

        animator.SetTrigger(DieHash);
    }

    #endregion
}