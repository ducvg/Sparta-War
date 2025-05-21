using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string bgmGroupName = "BGM";

    [SerializeField] private float minAudioValue = -80f;
    [SerializeField] private Slider slider;



    void OnEnable()
    {
        if (audioMixer.GetFloat(bgmGroupName, out float currentDb))
        {
            if (currentDb < minAudioValue) currentDb = minAudioValue;
            float normalized = Mathf.InverseLerp(minAudioValue, 0f, currentDb);
            float sliderValue = Mathf.Lerp(slider.minValue, slider.maxValue, normalized);
            slider.value = sliderValue;
        }
        else
        {
            slider.value = 1f;
        }
    }

    public void UpdateAudioVolume(float arg0)
    {
        float normalizedValue = Mathf.InverseLerp(slider.minValue, slider.maxValue, arg0);
        float dB = Mathf.Lerp(minAudioValue, 0f, normalizedValue);
        if (dB <= minAudioValue) dB = -80f;
        audioMixer.SetFloat(bgmGroupName, dB);
    }
}
