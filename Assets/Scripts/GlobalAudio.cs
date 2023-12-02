using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudio : MonoBehaviour
{
    public static GlobalAudio Instance { get; private set; }

    private AudioSource _audioSource;
    
    // Global audio level
    public float volume = 1.0f;
    
    // Is audio globally muted
    public bool isMuted = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            GameObject.Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);

        _audioSource = GetComponent<AudioSource>();
        
        Debug.Log("Global Audio Manager instantiated");
    }
    
    
    // Plays an audio clip once, globally
    public void PlayAudio(AudioClip audio, bool overrideMute = false)
    {
        if (isMuted && !overrideMute)
            return;
        
        _audioSource.PlayOneShot(audio, volume);
    }
}
