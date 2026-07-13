using UnityEngine;

public enum ItemType
{
    Weapon,
    Heal,
    Grenade
}

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private ItemType itemType;

    [Header("Weapon")]
    [SerializeField] private int weaponIndex;

    [Header("Heal")]
    [SerializeField] private float healAmount = 25f;

    [Header("Grenade")]
    [SerializeField] private int grenadeAmount = 1;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        switch (itemType)
        {
            case ItemType.Weapon:
                PickupWeapon(other);
                break;

            case ItemType.Heal:
                PickupHeal(other);
                break;

            case ItemType.Grenade:
                PickupGrenade(other);
                break;
        }

        gameObject.SetActive(false);
    }

    private void PickupWeapon(Collider other)
    {
        WeaponController controller = other.GetComponentInParent<WeaponController>();

        if (controller == null)
            return;

        controller.UnlockWeapon(weaponIndex);
        Debug.Log("Picked Gun");
    }

    private void PickupHeal(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Heal(healAmount);
            Debug.Log($"Heal: {healAmount}");

        }
    }

    private void PickupGrenade(Collider other)
    {
        WeaponController controller = other.GetComponentInParent<WeaponController>();

        if (controller == null)
            return;

        controller.AddGrenade(grenadeAmount);
        Debug.Log("Picked Grenade");

    }
}