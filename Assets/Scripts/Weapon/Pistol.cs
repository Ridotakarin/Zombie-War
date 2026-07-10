using UnityEngine;

public class Pistol : WeaponBase
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;


    protected override void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(
                bulletPrefab,
                firePoint.position,
                firePoint.rotation
            );
        }

        Debug.Log($"Pistol fired! Remaining ammo: {currentAmmo}");
    }
}