using UnityEngine;

public class Pistol : WeaponBase
{
    [Header("References")]
    [SerializeField] private Transform firePoint;

    [SerializeField] private LayerMask hitMask = ~0;

    private void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(firePoint.position, firePoint.forward * weaponData.range);
    }
    protected override void Fire()
    {
        Vector3 direction = GetFireDirection();

        if (Physics.Raycast(firePoint.position, direction, out RaycastHit hit, weaponData.range, hitMask))
        {
            Debug.DrawLine(firePoint.position, hit.point, Color.red, 1f);

            Debug.Log($"Hit : {hit.collider.name}");

            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(weaponData.damage);
            }

            // TODO
            // Hit Effect
        }
        else
        {
            Debug.DrawRay(firePoint.position, direction * weaponData.range, Color.green, 1f);
            Debug.Log("Missed");
        }

        // TODO
        // Muzzle Flash
        // Audio
    }

    private Vector3 GetFireDirection()
    {
        Vector3 direction = firePoint.forward;

        if (weaponData.spreadAngle <= 0) return direction;

        float angle = Random.Range(-weaponData.spreadAngle,weaponData.spreadAngle);

        return Quaternion.AngleAxis(angle, Vector3.up) * direction;
    }
    
}