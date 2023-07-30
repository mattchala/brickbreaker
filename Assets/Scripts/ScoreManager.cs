using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TMP_Text playerScoreText;
    public TMP_Text AIScoreText;
    public int playerScore = 0;
    public int AIScore = 0;
    

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScoreText.text = "Score: " + playerScore;
        AIScoreText.text = "Score: " + AIScore;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int points, int playerNumber)
    {
        if (playerNumber == 1)
        {
            playerScore += points;
            playerScoreText.SetText("Score: " + playerScore.ToString());
        }
        if (playerNumber == 2)
        {
            AIScore += points;
            AIScoreText.SetText("Score: " + AIScore.ToString());
        }
    }
}
