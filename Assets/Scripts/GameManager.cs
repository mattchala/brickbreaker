using UnityEngine;
using UnityEngine.SceneManagement;  // gives us access to unity library called scene manager we can use to load / change levels and scenes

public class GameManager : MonoBehaviour
{

    public int score = 0;
    public int lives = 3;
    public int level = 1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);     //  this will persist across loaded and deleted scenes
    }

    private void Start()
    {
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

}
