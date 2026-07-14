using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider healthSlider;

    private void OnEnable() => player.OnHealthChanged += UpdateHealthBar;
    private void OnDisable() => player.OnHealthChanged -= UpdateHealthBar;

    private void Start() => UpdateHealthBar(player.CurrentHealth, player.MaxHealth);

    private void UpdateHealthBar(float current, float max)
    {
        healthSlider.value = max > 0f ? current / max : 0f;
    }
}