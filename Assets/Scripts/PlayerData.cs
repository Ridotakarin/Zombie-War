using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Data Setting")]
    [Tooltip("Movement speed of the player character.")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 12f;

    [Tooltip("Health")]
    public int maxHealth = 100;

}
