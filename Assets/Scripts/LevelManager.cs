using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int level;

    // public static LevelManager Instance;

    static public int unlockedProgress=1;

    public void OpenScene()
    {
        GameManager.Instance.level = level;
        SceneManager.LoadScene("Level_" + level);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void RestartLevel()
    {
        GameManager.Instance.score = 0;
        GameManager.Instance.playerLives = 3;
        GameManager.Instance.AILives = 3;
        SceneManager.LoadScene("Level_" + (GameManager.Instance.level).ToString());
    }

    public void NextLevel()
    {
        GameManager.Instance.level ++;
        SceneManager.LoadScene("Level_" + (GameManager.Instance.level).ToString());
    }

    public void AI_Level(){
        //SceneManager.LoadScene("AI_Level_1");
        SceneManager.LoadScene("AI_Level_7");
    }
}
