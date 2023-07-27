using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera playerCamera;
    public Camera AICamera;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("GameMode") == 0)
        {
            AICamera.enabled = false;
            playerCamera.rect = new Rect(0, 0, 1, 1);
            playerCamera.orthographicSize = 10.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
