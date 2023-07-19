using UnityEngine;
using UnityEngine.SceneManagement;  // gives us access to unity library called scene manager we can use to load / change levels and scenes

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int lives = 3;
    public int level = 1;

    private void Awake()
    {
        Instance = this;
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
        Destroy(GameObject.Find("MainMenuCanvas"));  // HASSAN: Overrides DontDestroyOnLoad to get rid of main menu on level load
        NewGame();
    }

    private void NewGame()
    {
        // reset score, lives, and start at new level
        this.score = 0;
        this.lives = 3;
        SceneManager.LoadScene("Level Selection");
    }

    private void LoadLevel(int selected_level)
    {
        this.level = selected_level;
        SceneManager.LoadScene("Level_" + selected_level);
    }

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
