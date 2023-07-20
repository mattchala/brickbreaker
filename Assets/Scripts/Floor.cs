using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    public ParticleSystem death_chunks;
    private CameraShake screen_shake;

    // MATT: Unity built-in 
    private void Start()
    {
        screen_shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }


    // MATT: handle collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            death_chunks.transform.position = collision.GetContact(0).point;
            death_chunks.Play();
            screen_shake.LifeLostShake();
        }
    }
}
