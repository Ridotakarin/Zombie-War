using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;

    protected int currentAmmo;
    protected float lastFireTime;

    protected virtual void Awake()
    {
        if (weaponData == null)
        {
            Debug.LogError($"{name} is missing WeaponData.");
            enabled = false;
            return;
        }

        currentAmmo = weaponData.magazineSize;
    }

    #region Fire
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
        bool hasAmmo =weaponData.infiniteAmmo || currentAmmo > 0;

        bool cooldownFinished =
            Time.time >= lastFireTime + weaponData.fireRate;

        return hasAmmo && cooldownFinished;
    }

    protected virtual void ConsumeAmmo()
    {
        if (!weaponData.infiniteAmmo)
            currentAmmo--;
    }

    protected virtual void StartCooldown()
    {
        lastFireTime = Time.time;
    }

    protected abstract void Fire();
    #endregion
    #region Helper
    protected virtual Vector3 GetFireDirection(Transform firePos)
    {
        Vector3 direction = firePos.forward;
        if (weaponData.spreadAngle <= 0f) return direction;

        float angle = Random.Range(-weaponData.spreadAngle, weaponData.spreadAngle);
        return Quaternion.AngleAxis(angle, Vector3.up) * direction;
    }
    protected virtual void PlayFireSound()
    {
        Debug.Log($"Firing {weaponData.weaponName} sound.");
        //if (weaponData.fireSound != null)
        //{
        //    AudioSource.PlayClipAtPoint(weaponData.fireSound, transform.position);
        //}
    }
    protected virtual void PlayMuzzleFlash(ParticleSystem muzzleFlash)
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
    }
    protected virtual void SpawnHitEffect()
    {
       Debug.Log("Play hit effect");
    }
    #endregion
    #region Reload
    public virtual void Reload()
    {
        currentAmmo = weaponData.magazineSize;
    }

    public int CurrentAmmo => currentAmmo;
    #endregion
}
