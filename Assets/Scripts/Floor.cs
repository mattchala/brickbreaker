using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    public ParticleSystem death_chunks;

    // MATT: handle collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            death_chunks.transform.position = collision.GetContact(0).point;
            death_chunks.Play();
        }
    }
}
