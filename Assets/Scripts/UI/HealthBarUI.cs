using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider followHealth;
    [SerializeField] private float lerpSpeed = 8f;

    private float targetValue = 1f;

    private void OnEnable() => player.OnHealthChanged += UpdateHealthBar;
    private void OnDisable() => player.OnHealthChanged -= UpdateHealthBar;

    private void Start()
    {
        UpdateHealthBar(player.CurrentHealth, player.MaxHealth);
        if (player.MaxHealth > 0f)
        {
            float initialValue = player.CurrentHealth / player.MaxHealth;
            healthSlider.value = initialValue;
            followHealth.value = initialValue;
            targetValue = initialValue;
        }
    }

    private void UpdateHealthBar(float current, float max)
    {
        targetValue = max > 0f ? current / max : 0f;
        healthSlider.value = targetValue;
    }

    private void Update()
    {
        if (Mathf.Abs(followHealth.value - targetValue) > 0.0001f)
        {
            followHealth.value = Mathf.Lerp(followHealth.value, targetValue, lerpSpeed * Time.deltaTime);
        }
        else
        {
            followHealth.value = targetValue;
        }
    }
}