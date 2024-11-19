using UnityEngine;

public class SCR_SoundManager : MonoBehaviour
{
    [SerializeField] SO_Music _music;
    public SO_Music music => _music;
    AudioSource _soundsAudioSource;
    float _volumeRef = 0.5f;
    public float volumeRef => _volumeRef;
    [SerializeField] AudioClip _soundButton;
    public AudioClip soundButton => _soundButton;
    [SerializeField] AudioClip _soundClic;
    public AudioClip soundClic => _soundClic;
    static SCR_SoundManager _instance;
    static public SCR_SoundManager instance => _instance;

    void Start()
    {
        if (_instance == null) _instance = this;
        else { Destroy(gameObject); return; }

        _soundsAudioSource = gameObject.AddComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        CreateAudioSources();
    }

    void CreateAudioSources()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        if (music.clip != null) music.InstantiateAudioSource(source);
    }

    public void PlaySound(AudioClip clip)
    {
        _soundsAudioSource.PlayOneShot(clip);
    }
}