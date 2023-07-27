using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    public TMP_Text playerLifeText;
    public TMP_Text AILifeText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLifeText.text = "Lives: " + GameManager.Instance.playerLives.ToString();
        AILifeText.text = "Lives: " + GameManager.Instance.AILives.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLives(int lives, int playerNumber)
    {
        if (playerNumber == 1)
        {
        playerLifeText.SetText("Lives: " + lives.ToString());
        }
        if (playerNumber == 2)
        {
        AILifeText.SetText("Lives: " + lives.ToString());
        }
    }
}
