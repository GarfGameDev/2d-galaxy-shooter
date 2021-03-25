using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip _powerupAudio;
    private AudioSource _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = _powerupAudio;
    }

    public void PlayAudio()
    {
        _audio.Play();
    }
}
