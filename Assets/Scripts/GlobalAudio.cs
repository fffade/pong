using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudio : MonoBehaviour
{
    public static GlobalAudio Instance { get; private set; }

    private AudioSource _audioSource;

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
    public void PlayAudio(AudioClip audio)
    {
        _audioSource.PlayOneShot(audio);
    }
}
