using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControlUI : MonoBehaviour
{
    /* Applied to a volume control UI panel */

    // Icon handles full mute / unmute
    [SerializeField] private Button audioIconButton;
    [SerializeField] private Image audioIconImage;

    [SerializeField] private Sprite audioUnmutedSprite,
                                    audioMutedSprite;
    
    // Slider handles specific audio level
    [SerializeField] private Slider audioSlider;


    void Update()
    {
        UpdateIcon();
        UpdateSlider();
    }

    /* Updates icon to reflect mute or not */
    private void UpdateIcon()
    {
        bool isAudioMuted = GlobalAudio.Instance.isMuted;

        audioIconImage.sprite = isAudioMuted ? audioMutedSprite : audioUnmutedSprite;
    }
    
    /* Updates slider to reflect audio levels */
    private void UpdateSlider()
    {
        audioSlider.SetValueWithoutNotify(GlobalAudio.Instance.volume);
    }
    
    /* Triggered when slider is moved */
    public void OnSliderChange(float newValue)
    {
        GlobalAudio.Instance.volume = newValue;
    }
    
    /* Triggered when button is pressed */
    public void OnAudioIconPressed()
    {
        GlobalAudio.Instance.isMuted = !GlobalAudio.Instance.isMuted;
    }
} 
