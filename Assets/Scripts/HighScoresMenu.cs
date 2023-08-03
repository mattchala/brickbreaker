using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresMenu : MonoBehaviour
{

    public int level;
    public int difficultyNum;
    string difficulty;

    public void OnClickBackButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickLevel()
    {
        SceneManager.LoadScene("Level_" + level + "_High_Scores_Difficulty");
    }

    public void OnClickDifficulty() 
    {
        if (difficultyNum == 1) 
        {
            difficulty = "Easy";
        }
        else if (difficultyNum == 2) 
        {
            difficulty = "Medium";
        }
        else if (difficultyNum == 3) 
        {
            difficulty = "Hard";
        }

        SceneManager.LoadScene("Level_" + level + "_High_Scores_" + difficulty);
    }

    public void OnClickBackButtonDifficulty()
    {
        SceneManager.LoadScene("Level_" + level + "_High_Scores_Difficulty");
    }

}
