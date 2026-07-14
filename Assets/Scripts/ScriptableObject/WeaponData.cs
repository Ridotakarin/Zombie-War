using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("General")]
    public string weaponName;

    [Header("Damage")]
    [Min(0)] public float damage = 10f;
    [Min(1)] public float range = 20f;
    [Tooltip("Seconds between shots")]
    [Min(0.01f)] public float fireRate = 0.25f;

    [Header("Ammo")]
    public bool infiniteAmmo;
    [Min(1)] public int magazineSize = 12;
    [Min(0)] public int maxAmmo = 120;

    [Header("Reload")]
    public float reloadTime = 1.5f;

    [Header("Accuracy")]
    [Tooltip("Random spread angle in degrees")]
    public float spreadAngle = 0f;

    [Header("Effects — Pool ID, phải khớp ID đăng ký trong PoolManager")]
    public string muzzleFlashID;
    public string defaultImpactID;
    public string fleshImpactID;
    public string objectImpactID;
}