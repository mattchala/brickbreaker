using UnityEngine;
using UnityEngine.SceneManagement;  // gives us access to unity library called scene manager we can use to load / change levels and scenes

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int playerLives = 3;
    public int AILives = 3;
    public int level = 1;


    public Brick[] bricks { get; private set; }

    private void Awake()
    {
        Instance = this;
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        // HASSAN: Load up main menu if current scene is Global
        Scene loadedScene = SceneManager.GetActiveScene();
        string scene = loadedScene.name;
        if (scene == "Global") 
        {
            SceneManager.LoadScene("Menu");
        }
    }

    // HASSAN: Loads up Level_1 when the play button is pressed on main menu.
    public void OnClickPlay() 
    {
        NewGame();
    }

    public void NewGame()
    {
        // reset score, lives, and start at new level
        this.score = 0;
        this.playerLives = 3;
        this.AILives = 3;
        SceneManager.LoadScene("Level_Selection");
    }

    public static void LoadLevel(int selected_level)
    {
        // this.level = selected_level;
        SceneManager.LoadScene("Level_" + selected_level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        Instance.bricks = FindObjectsOfType<Brick>();
    }

    // MICHAEL: returns true if all bricks in level are deactivated
    public bool CheckBricks()
    {
        for (int i=0; i < Instance.bricks.Length; i++)
        {
            if (Instance.bricks[i].gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    public void levelComplete()
    {    
        if (GameManager.Instance.CheckBricks())
        {
            LevelManager.unlockedProgress = Mathf.Max(LevelManager.unlockedProgress, GameManager.Instance.level+1);
            Debug.Log(LevelManager.unlockedProgress);
            UpdateHighScore();
            SceneManager.LoadScene("Level_Complete");
        
            // // below is for resetting bricks during training
            for (int i=0; i < Instance.bricks.Length; i++)
            {
                Instance.bricks[i].gameObject.SetActive(true);
                Instance.bricks[i].brick_health = 2;    // changed to 2 from 3 to accomodate new brick health
            }
        }
    }

    public void LosePlayerLife()
    {
        playerLives -= 1;
        LifeManager.Instance.UpdateLives(playerLives, 1);
        if (playerLives <= 0)
        {
            // Game over
            UpdateHighScore();
            SceneManager.LoadScene("Game_Over");
        }
    }

    public void LoseAILife()
    {
        AILives -= 1;
        LifeManager.Instance.UpdateLives(AILives, 2);
        if (AILives <= 0)
        {
            // AI loss
        }
    }

     public void UpdateHighScore()
    {
        // Get player name
        string name = PlayerPrefs.GetString("PlayerName");

        // Check what level was being played
        Scene loadedScene = SceneManager.GetActiveScene();
        string scene = loadedScene.name;
        
        // Update high score table for specified level
        if (scene == "Level_1") 
        {
            L1HighScores.Instance.NewHighScore(ScoreManager.Instance.playerScore, name);
        }
        else if (scene == "Level_2")
        {
            L2HighScores.Instance.NewHighScore(ScoreManager.Instance.playerScore, name);
        }
        else if (scene == "Level_3")
        {
            L3HighScores.Instance.NewHighScore(ScoreManager.Instance.playerScore, name);
        }
    }

    public void OnClickHighScores()
    {
        SceneManager.LoadScene("High_Scores_Menu");
    }

    public void OnClickSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnClickHelp()
    {
        SceneManager.LoadScene("Help");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    // HASSAN: Game app quits when exit button is pressed on main menu
    public void OnClickExit() 
    {
        Debug.Log("EXIT");  // HASSAN: Only for debug purposes to verify exit functionality in editor
        Application.Quit();
    }

}
