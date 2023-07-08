using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator shake_animator;

    public void BrickShake()
    {
        // MATT: Eventually do a random selection function like "SelectBrickShake"
        shake_animator.Play("brick_shake_1");
    }

    // MATT: Unity built-in 
    private void Start()
    {
        Invoke(nameof(BrickShake), 1f); 
    }

}
