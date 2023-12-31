using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    
    public string playerName;
    public GameObject inputFieldPanel;
    public TMP_InputField inputField;

    void Start()
    {
        PlayerPrefs.SetString("PlayerName", "???");
        PlayerPrefs.Save();
    }

    public void OnClickEnter()
    {
        playerName = inputField.text;
        inputFieldPanel.SetActive(false);
        if (playerName == "")
        {
            PlayerPrefs.SetString("PlayerName", "???");
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", playerName);
        }
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("PlayerName"));
    }

}
