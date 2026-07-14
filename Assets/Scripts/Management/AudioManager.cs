using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SfxVolumeKey = "SfxVolume";

    public float MusicVolume => musicSource.volume;
    public float SfxVolume => sfxSource.volume;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource.volume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        sfxSource.volume = PlayerPrefs.GetFloat(SfxVolumeKey, 1f);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
    }

    public void SetSfxVolume(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat(SfxVolumeKey, value);
    }
}