using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerInputHandler input;
    [SerializeField] private Animator animator;

    private CharacterController characterController;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
        Rotate();
        UpdateAnimation();
    }

    private void Move()
    {
        Vector2 move = input.MoveInput;

        Vector3 direction = new Vector3(move.x, 0f, move.y);

        characterController.Move(
            direction * playerData.moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector2 move = input.MoveInput;

        if (move.sqrMagnitude < 0.01f)
            return;

        Vector3 lookDirection = new Vector3(move.x, 0f, move.y);

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            playerData.rotationSpeed * Time.deltaTime);
    }


    private void UpdateAnimation()
    {
        float speed = characterController.velocity.magnitude;
        animator.SetFloat(SpeedHash, speed);
    }
}