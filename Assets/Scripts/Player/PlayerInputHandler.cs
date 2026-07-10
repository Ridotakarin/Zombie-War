using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FixedJoystick moveJoystick;

    public Vector2 MoveInput { get; private set; }

    public bool IsFireHeld { get; private set; }
    public bool ReloadPressed { get; private set; }
    public bool SwitchPressed { get; private set; }
    public bool BombPressed { get; private set; }

    private void Update()
    {
        // Luôn lấy input từ joystick
        MoveInput = moveJoystick.Direction;
    }

    #region UI Events
    public void OnFireDown() => IsFireHeld = true;
    public void OnFireUp() => IsFireHeld = false;
    public void OnReload() => ReloadPressed = true;
    public void OnSwitchWeapon() => SwitchPressed = true;
    public void OnBomb() => BombPressed = true;
    #endregion

    private void LateUpdate()
    {
        ReloadPressed = false;
        SwitchPressed = false;
        BombPressed = false;
    }
}