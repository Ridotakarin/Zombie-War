using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;

    protected int currentAmmo;      // Đạn trong băng
    protected int reserveAmmo;      // Đạn dự trữ

    protected float lastFireTime;

    protected virtual void Awake()
    {
        if (weaponData == null)
        {
            Debug.LogError($"{name} is missing WeaponData.");
            enabled = false;
            return;
        }

        ResetAmmo();
    }

    #region Fire

    public bool TryFire()
    {
        if (!CanFire())
            return false;

        ConsumeAmmo();
        StartCooldown();
        Fire();
        return true;
    }

    protected virtual bool CanFire()
    {
        bool hasAmmo = weaponData.infiniteAmmo || currentAmmo > 0;
        bool cooldownFinished = Time.time >= lastFireTime + weaponData.fireRate;
        return hasAmmo && cooldownFinished;
    }

    protected virtual void ConsumeAmmo()
    {
        if (!weaponData.infiniteAmmo)
            currentAmmo--;
    }

    protected virtual void StartCooldown() => lastFireTime = Time.time;

    protected abstract void Fire();

    #endregion

    #region Helper

    protected virtual Vector3 GetFireDirection(Transform firePos)
    {
        Vector3 direction = firePos.forward;

        if (weaponData.spreadAngle <= 0f)
            return direction;

        float angle = Random.Range(-weaponData.spreadAngle, weaponData.spreadAngle);
        return Quaternion.AngleAxis(angle, Vector3.up) * direction;
    }

    protected virtual void PlayFireSound()
    {
        Debug.Log($"Firing {weaponData.weaponName}");
    }

    protected virtual void SpawnEffect(string poolID, Vector3 position, Quaternion rotation, float releaseDelay)
    {
        if (string.IsNullOrEmpty(poolID))
            return;

        PoolObject obj = PoolManager.Instance.Spawn(poolID);
        if (obj == null)
            return;

        obj.transform.SetPositionAndRotation(position, rotation); // bước bắt buộc, không được thiếu

        // Chạy trên PoolManager (singleton luôn active) — KHÔNG chạy trên
        // súng này, vì SwitchWeapon() SetActive(false) súng cũ sẽ huỷ ngang
        // coroutine, làm particle rò rỉ vĩnh viễn khỏi pool.
        PoolManager.Instance.StartCoroutine(ReleaseAfter(obj, releaseDelay));
    }

    private static IEnumerator ReleaseAfter(PoolObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        PoolManager.Instance.Release(obj);
    }

    protected virtual void PlayMuzzleFlash(Transform firePoint, Transform muzzlePoint)
    {
        SpawnEffect(weaponData.muzzleFlashID, firePoint.position, muzzlePoint.rotation, 0.12f);
    }

    protected virtual void SpawnHitEffect(RaycastHit hit)
    {
        string effectID = weaponData.defaultImpactID;

        if (hit.collider.TryGetComponent(out Surface surface))
        {
            switch (surface.SurfaceType)
            {
                case SurfaceType.Flesh:
                    effectID = weaponData.fleshImpactID;
                    break;
                case SurfaceType.Object:
                    effectID = weaponData.objectImpactID;
                    break;
            }
        }

        SpawnEffect(effectID, hit.point, Quaternion.LookRotation(hit.normal), 1f);
    }

    #endregion

    #region Reload

    public virtual void Reload()
    {
        if (weaponData.infiniteAmmo) return;
        if (currentAmmo >= weaponData.magazineSize) return;
        if (reserveAmmo <= 0) return;

        int needAmmo = weaponData.magazineSize - currentAmmo;
        int reloadAmount = Mathf.Min(needAmmo, reserveAmmo);

        currentAmmo += reloadAmount;
        reserveAmmo -= reloadAmount;
    }

    public virtual void ResetAmmo()
    {
        currentAmmo = weaponData.magazineSize;
        reserveAmmo = weaponData.maxAmmo;
    }

    #endregion

    #region Properties

    public WeaponData Data => weaponData;
    public float FireRate => weaponData.fireRate;
    public int CurrentAmmo => currentAmmo;
    public int ReserveAmmo => reserveAmmo;

    #endregion
}