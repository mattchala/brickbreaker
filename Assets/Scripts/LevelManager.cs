using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public int level;
    // public Text levelText;
    
    // // Start is called before the first frame update
    // void Start()
    // {
    //     levelText.text = level.ToString();
    // }

    public void OpenScene() {
        GameManager.Instance.level = level;
        SceneManager.LoadScene("Level_" + level);
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel(){
        GameManager.Instance.score = 0;
        GameManager.Instance.lives = 3;
        SceneManager.LoadScene("Level_" + (GameManager.Instance.level).ToString());
    }

    public void NextLevel(){
        GameManager.Instance.level ++;
        SceneManager.LoadScene("Level_" + (GameManager.Instance.level).ToString());
    }
}
