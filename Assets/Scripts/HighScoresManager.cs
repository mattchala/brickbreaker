using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresManager : MonoBehaviour
{
    // TODO: Saving, updating, loading high scores
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene("High_Scores_Menu");
    }

}
