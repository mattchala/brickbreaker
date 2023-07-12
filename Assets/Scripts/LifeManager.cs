using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    public TMP_Text lifeText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeText = GetComponent<TextMeshProUGUI>();
        lifeText.text = "Lives: " + GameManager.Instance.lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLives(int lives)
    {
        Debug.Log(lives);
        lifeText.text = "Lives: " + lives.ToString();
    }
}
