using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    private void Awake()
    {
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderValueChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderValueChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeSliderValueChanged);

        masterVolumeSlider.value = SerializeManager.GetFloat(FloatType.MasterVolume);
        sfxVolumeSlider.value = SerializeManager.GetFloat(FloatType.SfxVolume);
        musicVolumeSlider.value = SerializeManager.GetFloat(FloatType.MusicVolume);
    }

    private void OnMasterVolumeSliderValueChanged(float value)
    {
        SerializeManager.SetFloat(FloatType.MasterVolume, value);
    }

    private void OnSfxVolumeSliderValueChanged(float value)
    {
        SerializeManager.SetFloat(FloatType.SfxVolume, value);
    }

    private void OnMusicVolumeSliderValueChanged(float value)
    {
        SerializeManager.SetFloat(FloatType.MusicVolume, value);
    }
}