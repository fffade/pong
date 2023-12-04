using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Adds an audio click sound effect to a button UI component */
[RequireComponent(typeof(Button))]
public class ClickAudioUI : MonoBehaviour
{
    // The audio to play
    [SerializeField] private AudioClip clickAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GlobalAudio.Instance.PlayAudio(clickAudio);
        });
    }
}
