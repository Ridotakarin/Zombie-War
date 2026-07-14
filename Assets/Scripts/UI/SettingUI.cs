using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Graphics")]
    [SerializeField] private Toggle highQualityToggle;
    private const string QualityKey = "QualityLevel";

    private void OnEnable()
    {
        musicSlider.SetValueWithoutNotify(AudioManager.Instance.MusicVolume);
        sfxSlider.SetValueWithoutNotify(AudioManager.Instance.SfxVolume);

        int savedQuality = PlayerPrefs.GetInt(QualityKey, 1);
        highQualityToggle.SetIsOnWithoutNotify(savedQuality == 1);
    }

    public void OnMusicVolumeChanged(float value) => AudioManager.Instance.SetMusicVolume(value);
    public void OnSfxVolumeChanged(float value) => AudioManager.Instance.SetSfxVolume(value);

    public void OnQualityToggleChanged(bool isHighQuality)
    {
        int level = isHighQuality ? 1 : 0;
        QualitySettings.SetQualityLevel(level, true);
        PlayerPrefs.SetInt(QualityKey, level);
    }
}