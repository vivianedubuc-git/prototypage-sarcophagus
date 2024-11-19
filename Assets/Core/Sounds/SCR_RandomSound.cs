using UnityEngine;

public class SCR_RandomSound : MonoBehaviour
{
    private AudioSource _audioSource;
    private int _minDelay = 15;
    private int _maxDelay = 60;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        CalculateDelay();
    }

    private void PlaySound()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        CalculateDelay();
    }

    private void CalculateDelay()
    {
        int delay = Random.Range(_minDelay, _maxDelay); 
        Invoke("PlaySound", delay);
    }
}