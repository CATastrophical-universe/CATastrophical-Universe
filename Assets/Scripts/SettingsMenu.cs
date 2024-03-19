using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Slider playerSpeed, musicSlider, sfxSlider, masterSlider;
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    private static float speed;

    public void ChangeGrapichsLevel()
    {
        QualitySettings.SetQualityLevel(resolutionDropdown.value);
    }
    public void SetPlayerSpeed()
    {
        speed = playerSpeed.value;
    }
    public static float GetSpeed()
    {
        return speed;
    }
    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Param_VolMusic", musicSlider.value);
    }
    public void SetSFXVolume()
    {
        audioMixer.SetFloat("Param_VolSFX", sfxSlider.value);
    }
    public void SetMasterVolume()
    {
        audioMixer.SetFloat("Param_VolMaster", masterSlider.value);
    }

    // Start is for set all setings values
    void Start()
    {
        SetPlayerSpeed();
        ChangeGrapichsLevel();
        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
    }
}
