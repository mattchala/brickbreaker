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

    [SerializeField] private Slider particlesSlider = null;
    [SerializeField] private Text particlesTextUI = null;

    [SerializeField] private Slider screenShakeSlider = null;
    [SerializeField] private Text screenShakeTextUI = null;

    [SerializeField] private Slider playerDifficultySlider = null;
    [SerializeField] private Text playerDifficultyTextUI = null;

    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        float musicVolumeValue = PlayerPrefs.GetFloat("MusicVolume");
        float animationsOn = PlayerPrefs.GetFloat("AnimationsOn");
        float screenShakeOn = PlayerPrefs.GetFloat("ScreenShakeOn");
        float particlesOn = PlayerPrefs.GetFloat("ParticlesOn");
        float playerDifficulty = PlayerPrefs.GetFloat("PlayerDifficulty");
        
        musicVolumeSlider.value = musicVolumeValue;
        particlesSlider.value = particlesOn;
        animationsSlider.value = animationsOn;
        screenShakeSlider.value = screenShakeOn;
        playerDifficultySlider.value = playerDifficulty;
    }

    public void SaveSettings()
    {
        float musicVolume = musicVolumeSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        float animations = animationsSlider.value;
        PlayerPrefs.SetFloat("AnimationsOn", animations);

        float particles = particlesSlider.value;
        PlayerPrefs.SetFloat("ParticlesOn", particles);

        float screenShake = screenShakeSlider.value;
        PlayerPrefs.SetFloat("ScreenShakeOn", screenShake);

        float playerDifficulty = playerDifficultySlider.value;
        PlayerPrefs.SetFloat("PlayerDifficulty", playerDifficulty);
        
        LoadSettings();
    }

    public void MusicVolumeSlider(float musicVolume)
    {
        musicVolumeTextUI.text = musicVolume.ToString();
    }

    public void AnimationsSlider(float animations)
    {
        animationsTextUI.text = animations == 1.0 ? "On" : "Off";
    }

    public void ParticlesSlider(float particles)
    {
        particlesTextUI.text = particles == 1.0 ? "On" : "Off";
    }

    public void ScreenShakeSlider(float screenShake)
    {
        screenShakeTextUI.text = screenShake == 1.0 ? "On" : "Off";
    }

    public void PlayerDifficultySlider(float playerDifficulty)
    {
        switch(playerDifficulty) 
        {
        case 0.0f:
            playerDifficultyTextUI.text = "Easy";
            break;
        case 1.0f:
            playerDifficultyTextUI.text = "Medium";
            break;
        case 2.0f:
            playerDifficultyTextUI.text = "Hard";
            break;
        default:
            playerDifficultyTextUI.text = "Medium";
            break;
        }
    }
}
