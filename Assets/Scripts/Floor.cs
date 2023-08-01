using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    public ParticleSystem death_chunks;
    private CameraShake screen_shake;

    public PaddleAgent ai_paddle;   // MATT: trigger here to see if episode is ending

    // MATT: Unity built-in 
    private void Start()
    {
        screen_shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }


    // MATT: handle collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball" && PlayerPrefs.GetFloat("ScreenShakeOn") == 1)
        {
            death_chunks.transform.position = collision.GetContact(0).point;
            death_chunks.Play();
            screen_shake.LifeLostShake();
        }

        // MATT: call paddleagent function if it exists
        if (ai_paddle)
        {
            ai_paddle.BadReward();
        }
    }
}
