using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeController : MonoBehaviour
{
    [SerializeField] private Slider gameModeSlider = null;

    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        float gameModeValue = PlayerPrefs.GetFloat("GameMode");
        gameModeSlider.value = gameModeValue;
    }

    public void SaveSettings()
    {
        float gameMode = gameModeSlider.value;
        PlayerPrefs.SetFloat("GameMode", gameMode);
        
        LoadSettings();
    }
}
