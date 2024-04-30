using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider, sfxSlider, masterSlider;
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    public void ChangeGrapichsLevel()
    {
        QualitySettings.SetQualityLevel(resolutionDropdown.value);
        PlayerPrefs.SetInt("grapichsLevel", resolutionDropdown.value);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Param_VolMusic", musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", Mathf.Pow(10.0f, musicSlider.value / 20.0f));
        PlayerPrefs.Save();
    }
    public void SetSFXVolume()
    {
        audioMixer.SetFloat("Param_VolSFX", sfxSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", Mathf.Pow(10.0f, sfxSlider.value / 20.0f));
        PlayerPrefs.Save();
    }
    public void SetMasterVolume()
    {
        audioMixer.SetFloat("Param_VolMaster", masterSlider.value);
        PlayerPrefs.SetFloat("masterVolume", Mathf.Pow(10.0f, masterSlider.value / 20.0f));
        PlayerPrefs.Save();
    }

    // Start is for set all setings values
    void Start()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("grapichsLevel", 2);
        ChangeGrapichsLevel();
        musicSlider.value = Mathf.Log10(PlayerPrefs.GetFloat("musicVolume", 0.75f)) * 20;
        sfxSlider.value = Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume", 0.75f)) * 20;
        masterSlider.value = Mathf.Log10(PlayerPrefs.GetFloat("masterVolume", 0.75f)) * 20;
    }
}
