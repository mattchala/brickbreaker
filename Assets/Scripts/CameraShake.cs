using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator shake_animator;

    public void BrickShake()
    {
        shake_animator.Play("brick_shake_1");
    }

    public void PaddleShake()
    {
        shake_animator.Play("paddle_shake");
    }

    public void LeftWallShake()
    {
        shake_animator.Play("left_wall_shake");
    }

    public void RightWallShake()
    {
        shake_animator.Play("right_wall_shake");
    }
}
