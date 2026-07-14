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
    private static readonly int DieHash = Animator.StringToHash("Dead");
    private static readonly int HurtHash = Animator.StringToHash("Hurt");
    private static readonly int RifleHash = Animator.StringToHash("Rifle_Shoot");
    private static readonly int PistolHash = Animator.StringToHash("Pistol_Shoot");


    private static readonly int HandLayer = 1;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GetComponent<Player>();

        animator = GetComponent<Animator>();
        animator.SetLayerWeight(HandLayer, 1f);
        player.OnDead += OnPlayerDead;
        player.OnTakeDamage += OnTakeDamage;
    }

    private void OnDestroy()
    {
        if (player != null)
        { 
            player.OnDead -= OnPlayerDead;
            player.OnTakeDamage -= OnTakeDamage;
        }
        

    }

    private void Update()
    {
        if (player.IsDead)
            return;
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying) return;


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
            bool fired = weaponController.Fire();
            if(fired)
            {
                if (weaponController.CurrentFireRate == 0.1f)
                {
                    animator.Play(RifleHash);
                }
                else
                {
                    animator.Play(PistolHash);
                }
                weaponController.Fire();
            }
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
        animator.SetLayerWeight(HandLayer, 0f);
        animator.ResetTrigger(HurtHash);
        characterController.enabled = false;
        animator.SetTrigger(DieHash);
    }
    private void OnTakeDamage()
    {
        if (player.IsDead)
            return;

        animator.SetTrigger(HurtHash);
    }

    #endregion
}