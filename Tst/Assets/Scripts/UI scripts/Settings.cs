using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour

{
    public AudioMixer _audio;

    public void AudioVolume(float sliderValue)
    {
        _audio.SetFloat("masterVolume", Mathf.Log10(sliderValue)*20);
        
    }

    
}
