/* The following code is adapted from the tutorial "High Score Table with Saving and Loading (Unity
Tutorial for Beginners)" at the following URL: https://www.youtube.com/watch?v=iAbaqGYdnyI */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelHighScoresSystem : MonoBehaviour
{
    
    public static LevelHighScoresSystem Instance;
    public int level;
    public int difficultyNum;
    string difficulty;

    // References to table, container, and template for high scores
    private Transform highScoresTable;
    private Transform highScoresContainer;
    private Transform highScoresTemplate;

    // Reference to high scores list
    private List<Transform> highScoresEntryTransformList;

    private void Awake()
    {   
        Instance = this;

        Scene loadedScene = SceneManager.GetActiveScene();
        string scene = loadedScene.name;

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
        
        if (scene == "Level_" + level.ToString() + "_High_Scores_" + difficulty)
        {
            // Find table, container, and template objects and update references
            highScoresTable = transform.Find("HighScoresTable");
            highScoresContainer = highScoresTable.Find("HighScoresContainer");
            highScoresTemplate = highScoresContainer.Find("HighScoresTemplate");
            
            // Hide high scores template on scene load
            highScoresTemplate.gameObject.SetActive(false);
        

            // Get high scores list and convert to string
            string jsonHighScoresString = PlayerPrefs.GetString("Level_" + level.ToString() + "_High_Scores_Table_" + difficulty);
            HighScores highScores = JsonUtility.FromJson<HighScores>(jsonHighScoresString);

            // Create new high scores list if none exists
            if (highScores == null)
            {
                highScores = new HighScores();
            }

            // Create new high scores entry if none exists
            if (highScores.highScoresEntryList == null)
            {
                highScores.highScoresEntryList = new List<HighScoresEntry>();
            }

            // Sort the high score list in descending order
            highScores.highScoresEntryList.Sort((firstScore, secondScore) => secondScore.score.CompareTo(firstScore.score));
            
            // Create a list of transforms for each high score table entry
            highScoresEntryTransformList = new List<Transform>();
            foreach (HighScoresEntry highScoresEntry in highScores.highScoresEntryList) 
            {
                CreateHighScoresTransform(highScoresEntry, highScoresContainer, highScoresEntryTransformList);
            }
        }
    }

    private void CreateHighScoresTransform(HighScoresEntry highScoresEntry, Transform highScoresContainer, List<Transform> transformList)
    {
        // Set template height
        float highScoresTemplateHeight = 23.0f;

        // Create a new high scores template
        Transform highScoresTransform = Instantiate(highScoresTemplate, highScoresContainer);

        // Get the rect transform of high scores UI and create a vector position for new template
        RectTransform highScoresRectTransform = highScoresTransform.GetComponent<RectTransform>();
        highScoresRectTransform.anchoredPosition = new Vector2(0, -highScoresTemplateHeight * transformList.Count);

        // Make the high scores visible
        highScoresTransform.gameObject.SetActive(true);
        
        // Increment player rank and set "RankText" to next rank
        int playerRank = transformList.Count + 1;
        string playerRankString = playerRank.ToString();
        highScoresTransform.Find("RankText").GetComponent<TMP_Text>().text = playerRankString;

        // Get score value and set "ScoreText" to it
        int score = highScoresEntry.score;
        string scoreString = score.ToString();
        highScoresTransform.Find("ScoreText").GetComponent<TMP_Text>().text = scoreString;

        // Get player name and set "PlayerText" to it
        string playerName = highScoresEntry.playerName;
        highScoresTransform.Find("PlayerText").GetComponent<TMP_Text>().text = playerName;

        // Add the new transform to list of transforms
        transformList.Add(highScoresTransform);
    }

    public void NewHighScore(int score, string playerName, string level_num, string difficultyLevel)
    {
        // Create new entry for high scores
        HighScoresEntry highScoresEntry = new HighScoresEntry{score = score, playerName = playerName};
        
        // Load the previously saved high scores
        string jsonHighScoresString = PlayerPrefs.GetString("Level_" + level_num + "_High_Scores_Table_" + difficultyLevel);
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonHighScoresString);

        // Create new high scores list if none exists
        if (highScores == null)
        {
            highScores = new HighScores();
        }

        // Create new high scores entry if none exists
        if (highScores.highScoresEntryList == null)
        {
            highScores.highScoresEntryList = new List<HighScoresEntry>();
        }

        // Sort before adding score
        highScores.highScoresEntryList.Sort((firstScore, secondScore) => secondScore.score.CompareTo(firstScore.score));

        // Add new high score entry and limit table to top 10 scores
        if (highScores.highScoresEntryList.Count == 0 && score > 0)
        {
            highScores.highScoresEntryList.Add(highScoresEntry);
        }
        else 
        {
            int addScore = 1;
            for (int i = 0; i < highScores.highScoresEntryList.Count; i++)
            {
                if (highScores.highScoresEntryList[i].score >= score && highScores.highScoresEntryList[i].playerName == playerName)
                {
                    addScore = 0;
                    break;
                }
            }

            if (addScore == 1 && score > 0 && highScores.highScoresEntryList.Count < 10)
            {
                highScores.highScoresEntryList.Add(highScoresEntry);
            }
            else if (addScore == 1 && score > 0 && highScores.highScoresEntryList.Count == 10) 
            {
                int lastScore = highScores.highScoresEntryList[9].score;
                if (score > lastScore && score > 0)
                {
                    highScores.highScoresEntryList.RemoveAt(9);
                    highScores.highScoresEntryList.Add(highScoresEntry);
                }
            }
            else
            {
                addScore = 1;
            }

            // Sort after adding score
            highScores.highScoresEntryList.Sort((firstScore, secondScore) => secondScore.score.CompareTo(firstScore.score));
        }
        
        // Convert updated list to JSON and save to PlayerPrefs
        string jsonHighScoresEntryList = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("Level_" + level_num + "_High_Scores_Table_" + difficultyLevel, jsonHighScoresEntryList);
        PlayerPrefs.Save();
    }

    // Class representing list of high scores
    private class HighScores
    {
        public List<HighScoresEntry> highScoresEntryList;
    } 

    // Class representing a high score with score and player name attributes
    [System.Serializable]
    private class HighScoresEntry 
    { 
        public int score;
        public string playerName;
    }

    // Back button click goes to high scores menu
    public void OnClickBackButton() 
    {
        SceneManager.LoadScene("High_Scores_Menu");
    }

    // Reset button to clear out high scores table
    public void OnClickReset()
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

        PlayerPrefs.DeleteKey("Level_" + level.ToString() + "_High_Scores_Table_" + difficulty);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level_" + level.ToString() + "_High_Scores_" + difficulty);
    }

}
