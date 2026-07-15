using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private TMP_Text ammoText;

    private int lastCurrent = -1;
    private int lastReserve = -1;

    private void Update()
    {
        int current = weaponController.CurrentAmmo;
        int reserve = weaponController.ReserveAmmo;

        if (current == lastCurrent && reserve == lastReserve)
            return; // không rebuild string/UI nếu số không đổi

        lastCurrent = current;
        lastReserve = reserve;
        ammoText.text = $"{current} / {reserve}";
    }
}