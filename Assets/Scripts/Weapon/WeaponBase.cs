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
    protected virtual void SpawnEffect(ParticleSystem prefab, Vector3 position, Quaternion rotation, float destroyTime)
    {
        if (prefab == null)
            return;

        ParticleSystem effect = Instantiate(prefab, position, rotation);

        effect.Play();

        Destroy(effect.gameObject, destroyTime);
    }
    protected virtual void PlayMuzzleFlash(Transform firePoint, Transform muzzlePoint)
    {
        SpawnEffect(weaponData.muzzleFlash, firePoint.position, muzzlePoint.rotation, 0.15f);
    }
    protected virtual void SpawnHitEffect(RaycastHit hit)
    {
        ParticleSystem effect = weaponData.defaultImpact;

        if (hit.collider.TryGetComponent(out Surface surface))
        {
            switch (surface.SurfaceType)
            {
                case SurfaceType.Flesh:
                    effect = weaponData.fleshImpact;
                    break;

                case SurfaceType.Object:
                    effect = weaponData.objectImpact;
                    break;
                default:
                    effect = weaponData.defaultImpact;
                    break;
            }
        }

        SpawnEffect(effect, hit.point, Quaternion.LookRotation(hit.normal),1f);
        Debug.Log($"Hit {effect}.");
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
