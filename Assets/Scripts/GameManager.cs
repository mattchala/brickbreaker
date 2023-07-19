using UnityEngine;
using UnityEngine.SceneManagement;  // gives us access to unity library called scene manager we can use to load / change levels and scenes

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int lives = 3;
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
        this.lives = 3;
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

    public bool Cleared()
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

    // public static void LevelCompleted()
    // {
    //     NewGame();
    // }
    

    public void LoseLife()
    {
        lives -= 1;
        Debug.Log("Life lost");
        LifeManager.Instance.UpdateLives(lives);
        if (lives <= 0)
        {
            Debug.Log("Game Over");
            // Game over
        }
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
