using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused) 
            {
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
        }

        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ResetGame();
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void PauseGame() 
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    private void ResumeGame() 
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    private void ResetGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
    }

}
