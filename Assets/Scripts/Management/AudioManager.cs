using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        // Chỉ cần nếu bạn có nhu cầu reload lại scene (nút Retry) sau này;
        // nếu game chỉ chạy 1 scene xuyên suốt, có thể bỏ dòng này.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (backgroundMusic != null)
            PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }
}