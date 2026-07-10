using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Data")]
    [SerializeField] protected WeaponData weaponData;

    protected int currentAmmo;
    protected float lastFireTime;


    protected virtual void Awake()
    {
        if (weaponData == null)
        {
            Debug.LogError($"{name} missing WeaponData.");
            enabled = false;
            return;
        }

        currentAmmo = weaponData.magazineSize;
    }
    public void TryFire()
    {
        if (!CanFire())
            return;

        ConsumeAmmo();
        StartCooldown();

        Fire();
    }

    protected virtual bool CanFire()
    {
        bool hasAmmo = weaponData.infiniteAmmo || currentAmmo > 0;

        bool cooldownFinished =
            Time.time >= lastFireTime + weaponData.fireRate;


        return hasAmmo && cooldownFinished;
    }

    protected virtual void ConsumeAmmo()
    {
        if (!weaponData.infiniteAmmo)
        {
            currentAmmo--;
        }
    }

    protected virtual void StartCooldown()
    {
        lastFireTime = Time.time;
    }
    protected abstract void Fire();

    public virtual void Reload()
    {
        currentAmmo = weaponData.magazineSize;
    }

    public int CurrentAmmo => currentAmmo;

    public WeaponData WeaponData => weaponData;
}