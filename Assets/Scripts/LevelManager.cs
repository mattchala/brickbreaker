using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public int level;
    public Text levelText;
    
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = level.ToString();
    }

    public void OpenScene() {
        SceneManager.LoadScene("Level_" + level);
    }
}
