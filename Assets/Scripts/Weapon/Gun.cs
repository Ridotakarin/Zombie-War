using UnityEngine;

public class Gun : WeaponBase
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform muzzlePoint;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask hitMask = ~0;

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null || weaponData == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(firePoint.position, firePoint.forward * weaponData.range);
    }

    protected override void Fire()
    {
        PlayMuzzleFlash(firePoint,muzzlePoint);
        PlayFireSound();

        Vector3 direction = GetFireDirection(firePoint);

        if (Physics.Raycast(firePoint.position, direction,
            out RaycastHit hit, weaponData.range, hitMask))
        {
            Debug.DrawLine(firePoint.position, hit.point, Color.red, 1f);

            OnHit(hit);
        }
        else
        {
            OnMiss(direction);
        }
    }

    private void OnHit(RaycastHit hit)
    {
        Debug.Log($"Hit : {hit.collider.name}");

        SpawnHitEffect(hit);

        if (hit.collider.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(weaponData.damage);
        }
        if(hit.collider.TryGetComponent(out Grenade bomb))
        {
            bomb.Explode();
        }
    }

    private void OnMiss(Vector3 direction)
    {
        Debug.DrawRay(firePoint.position, direction * weaponData.range, Color.green, 1f);
        Debug.Log("Missed");
    }
}