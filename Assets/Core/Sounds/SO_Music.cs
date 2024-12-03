using UnityEngine;

[CreateAssetMenu(menuName = "Music", fileName = "Music")]
public class SO_Music : ScriptableObject
{
    [SerializeField] AudioClip _clip;
    public AudioClip clip => _clip;
    AudioSource _source;
    public AudioSource source => _source;

    public void InstantiateAudioSource(AudioSource source)
    {
        _source = source;
        _source.clip = _clip;
        _source.loop = true;
        _source.playOnAwake = false;
        _source.Play();
        AdjustVolume(0);
    }

    public void AdjustVolume(int volume)
    {
        _source.volume = volume;
    }
}