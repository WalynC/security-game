using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource example;
    public Slider slider;

    public void SetVolume()
    {
        mixer.SetFloat("Volume", Mathf.Log10(slider.value) * 20);
        example.Play();
    }
}
