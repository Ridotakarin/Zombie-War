using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Name")]
    public string weaponName;

    [Header("Weapon Setting")]
    public float reloadTime = 2f;
    [Min(0f)]
    public float damage = 10f; 
    public float range = 100f;

    [Tooltip("Seconds between each shot")]
    public float fireRate = 0.5f; 

    [Header("Ammo")]
    public bool infiniteAmmo;
    public int magazineSize = 30;  
    public int maxAmmo = 120;

    [Header("FX")]
    public ParticleSystem muzzleFlash;
    public AudioClip fireSound;

    [Header("Projectile")]
    public Transform firePoint;
    public GameObject bulletPrefab;

}
