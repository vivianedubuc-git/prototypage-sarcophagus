using System.Collections;
using System.Collections.Generic;
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
        AdjustVolume();
    }

    public void AdjustVolume()
    {
        if (SCR_MapManager.instance.GetSceneName() == "Game") _source.volume = SCR_SoundManager.instance.volumeRef;
        else _source.volume = 0;
    }
}