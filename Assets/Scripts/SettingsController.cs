using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Text musicVolumeTextUI = null;

    [SerializeField] private Slider animationsSlider = null;
    [SerializeField] private Text animationsTextUI = null;

    [SerializeField] private Slider screenShakeSlider = null;
    [SerializeField] private Text screenShakeTextUI = null;

    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        float musicVolumeValue = PlayerPrefs.GetFloat("MusicVolume");
        float animationsOn = PlayerPrefs.GetFloat("AnimationsOn");
        float screenShakeOn = PlayerPrefs.GetFloat("ScreenShakeOn");
        
        musicVolumeSlider.value = musicVolumeValue;
        animationsSlider.value = animationsOn;
        screenShakeSlider.value = screenShakeOn;
    }

    public void SaveSettings()
    {
        float musicVolume = musicVolumeSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        float animations = animationsSlider.value;
        PlayerPrefs.SetFloat("AnimationsOn", animations);

        float screenShake = screenShakeSlider.value;
        PlayerPrefs.SetFloat("ScreenShakeOn", screenShake);
        
        LoadSettings();
    }

    public void MusicVolumeSlider(float musicVolume)
    {
        musicVolumeTextUI.text = musicVolume.ToString();
    }

    public void AnimationsSlider(float animations)
    {
        animationsTextUI.text = animations.ToString();
    }

    public void ScreenShakeSlider(float screenShake)
    {
        screenShakeTextUI.text = screenShake.ToString();
    }
}
