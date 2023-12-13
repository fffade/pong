using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalAudio : MonoBehaviour
{
    public static GlobalAudio Instance { get; private set; }

    private AudioSource _audioSource;

    private Dictionary<AudioClip, AudioSource> _otherAudioSources;
    
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

        _otherAudioSources = new Dictionary<AudioClip, AudioSource>();
        
        Debug.Log("Global Audio Manager instantiated");
    }
    
    
    // Plays an audio clip once, globally
    public void PlayAudio(AudioClip audio, bool overrideMute = false)
    {
        if (isMuted && !overrideMute)
            return;

        _audioSource.clip = null;
        
        _audioSource.PlayOneShot(audio, volume);
    }
    
    // Plays an audio clip steadily, globally
    public void PlayLoopAudio(AudioClip audio, bool overrideMute = false)
    {
        if (isMuted && !overrideMute)
            return;

        if (_otherAudioSources.ContainsKey(audio))
        {
            DestroyAudioSource(audio);
        }

        AudioSource source = (AudioSource) gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        source.clip = audio;
        source.loop = true;
        
        _otherAudioSources.Add(audio, source);
        
        source.Play();
    }
    
    // Destroys a looping audio's source
    public void DestroyAudioSource(AudioClip audio)
    {
        if (!_otherAudioSources.ContainsKey(audio))
        {
            return;
        }
        
        GameObject.DestroyImmediate(_otherAudioSources[audio]);

        _otherAudioSources.Remove(audio);
    }
    
    // Stops a playing audio clip if matching
    public void StopLoopAudio(AudioClip audio)
    {
        if (_otherAudioSources.ContainsKey(audio))
        {
            AudioSource source = _otherAudioSources[audio];

            source.Stop();

            DestroyAudioSource(audio);
        }
    }
}
