using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresMenu : MonoBehaviour
{

    public int level;

    public void OnClickBackButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickLevel()
    {
        SceneManager.LoadScene("Level_" + level + "_High_Scores");
    }

}
