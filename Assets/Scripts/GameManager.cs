using UnityEngine;
using UnityEngine.SceneManagement;  // gives us access to unity library called scene manager we can use to load / change levels and scenes

public class GameManager : MonoBehaviour
{

    public int score = 0;
    public int lives = 3;
    public int level = 1;

    public bool isGlobal = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);     //  this will persist across loaded and deleted scenes
    }

    private void Start()
    {
        if (isGlobal) 
        {
            SceneManager.LoadScene("Menu");
        }
    }

    // HASSAN: Loads up Level_1 when the play button is pressed on main menu.
    public void OnClickPlay() 
    {
        Destroy(gameObject);  // Overrides DontDestroyOnLoad to get rid of main menu on level load
        NewGame();
    }

    private void NewGame()
    {
        // reset score, lives, and start at new level
        this.score = 0;
        this.lives = 3;
        LoadLevel(1);
    }

    private void LoadLevel(int selected_level)
    {
        this.level = selected_level;
        SceneManager.LoadScene("Level_" + selected_level);
    }

    // HASSAN: Game app quits when exit button is pressed on main menu
    public void OnClickExit() 
    {
        Debug.Log("EXIT");  // Only for debug purposes to verify exit functionality in editor
        Application.Quit();
    }

}
