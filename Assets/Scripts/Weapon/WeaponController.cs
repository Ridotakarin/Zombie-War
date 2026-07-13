using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private List<WeaponBase> weapons = new();

    [SerializeField]
    private int currentWeaponIndex;

    [Header("Grenade")]
    [SerializeField]
    private int grenadeCount;

    private bool[] unlockedWeapons;

    private void Awake()
    {
        unlockedWeapons = new bool[weapons.Count];

        // Mặc định chỉ có Pistol
        unlockedWeapons[0] = true;

        currentWeaponIndex = 0;

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    #region Weapon

    public void Fire()
    {
        if (weapons.Count == 0)
            return;

        weapons[currentWeaponIndex].TryFire();
    }

    public void Reload()
    {
        if (weapons.Count == 0)
            return;

        weapons[currentWeaponIndex].Reload();
    }

    public void SwitchWeapon()
    {
        if (weapons.Count <= 1)
            return;

        int next = currentWeaponIndex;

        do
        {
            next++;

            if (next >= weapons.Count)
                next = 0;

        } while (!unlockedWeapons[next] && next != currentWeaponIndex);

        EquipWeapon(next);
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Count)
            return;

        if (!unlockedWeapons[index])
            return;

        if (index == currentWeaponIndex)
            return;

        weapons[currentWeaponIndex].gameObject.SetActive(false);

        currentWeaponIndex = index;

        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void UnlockWeapon(int index)
    {
        if (index < 0 || index >= weapons.Count)
            return;

        unlockedWeapons[index] = true;

        // Nhặt súng => Full toàn bộ đạn
        weapons[index].ResetAmmo();

        EquipWeapon(index);

        Debug.Log($"Unlocked {weapons[index].Data.weaponName}");
    }

    #endregion

    #region Grenade

    public void AddGrenade(int amount)
    {
        grenadeCount += amount;
    }

    public void ThrowGrenade()
    {
        if (grenadeCount <= 0)
            return;

        grenadeCount--;
    }

    public int GrenadeCount => grenadeCount;

    #endregion
}