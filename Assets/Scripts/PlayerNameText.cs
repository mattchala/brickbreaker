using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameText : MonoBehaviour
{

    public TMP_Text playerNameText;

    // Start is called before the first frame update
    void Start()
    {
        string name = PlayerPrefs.GetString("PlayerName");
        if (name == null) 
        {
            PlayerPrefs.SetString("PlayerName", "???");
            PlayerPrefs.Save();
        }
        playerNameText.text = name;
    }

}
