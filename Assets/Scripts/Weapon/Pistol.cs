using UnityEngine;

public class Pistol : WeaponBase
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask hitMask = ~0;

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(firePoint.position, firePoint.forward * weaponData.range);
    }
    protected override void Fire()
    {
        PlayMuzzleFlash();
        PlayFireSound();
        Vector3 direction = GetFireDirection();

        if (Physics.Raycast(firePoint.position, direction, out RaycastHit hit, weaponData.range, hitMask))
        {
            Debug.DrawLine(firePoint.position, hit.point, Color.red, 1f);
            OnHit(hit);
        }
        else
        {
            OnMiss(direction);
        }
    }

    private Vector3 GetFireDirection()
    {
        Vector3 direction = firePoint.forward;

        if (weaponData.spreadAngle <= 0) return direction;

        float angle = Random.Range(-weaponData.spreadAngle, weaponData.spreadAngle);

        return Quaternion.AngleAxis(angle, Vector3.up) * direction;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash?.Play();
    }
    private void OnHit(RaycastHit hit)
    {
        Debug.Log($"Hit : {hit.collider.name}");
        SpawnHitEffect();
        if (hit.collider.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(weaponData.damage);
        }
    }
    private void SpawnHitEffect()
    {
        Debug.Log("Play hit effect");
    }
    private void OnMiss(Vector3 direction)
    {
        Debug.DrawRay(firePoint.position, direction * weaponData.range, Color.green, 1f);
        Debug.Log("Missed");
    }
    private void PlayFireSound()
    {
        Debug.Log("Play fire sound");
    }

}