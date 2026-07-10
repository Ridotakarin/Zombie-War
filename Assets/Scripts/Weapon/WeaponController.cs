using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponBase currentWeapon;

    public void Fire()
    {
        if (currentWeapon != null)
        {
            currentWeapon?.TryFire();
        }
    }
    public void Reload()
    {
        if (currentWeapon != null)
        {
            currentWeapon?.Reload();
        }
    }
}
