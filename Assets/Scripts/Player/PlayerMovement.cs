using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private WeaponController weaponController;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Rotate();

        DebugInput();
    }

    private void Move()
    {
        Vector2 input = inputHandler.MoveInput;

        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector2 input = inputHandler.MoveInput;

        if (input.sqrMagnitude < 0.01f)
            return;

        Vector3 lookDirection = new Vector3(input.x, 0f, input.y);

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }

    private void DebugInput()
    {
        if (inputHandler.IsFireHeld)
        {
            weaponController.Fire();
            Debug.Log("Holding Fire");
        }

        if (inputHandler.ReloadPressed)
            Debug.Log("Reload");

        if (inputHandler.SwitchPressed)
            Debug.Log("Switch Weapon");

        if (inputHandler.BombPressed)
            Debug.Log("Bomb");
    }
}