using UnityEngine;
using System;
using System.Collections;


// TODO : Limit/clamp horizontal angle ball can travel at
// TODO : Sometimes the ball slows down, figure out how to keep it's velocity consistent


public class Ball : MonoBehaviour
{
    public Rigidbody2D ball_body { get; private set; }
    public float speed = 1000f;
    private CameraShake screen_shake;
    public Animator ball_animator;    // MATT: this creates an empty slot in unity editor, drag the desired animator there
    public int minHorizontalAngle = 5;

    // MATT: Unity built-in 
    private void Awake()
    {
        // MATT: Cache objects into variables on load here
        this.ball_body = GetComponent<Rigidbody2D>();
    }


    // MATT: Unity built-in 
    private void Start()
    {
        screen_shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        // MATT: instead of immediately calling the set_trajectory function, we wait 1 second and then fire it off
        StartCoroutine("ResetBall");
    }


    // MATT: sets the ball's initial direction in a random angle, but always downward
    private IEnumerator ResetBall()
    {
        Vector2 force = Vector2.zero;
        //force.x = Random.Range(-1f, 1f);  // MATT: Comment this line out if you don't want the starting ball trajectory to be randomized
        force.y = -1f;
        transform.position = new Vector3(0, 0, 0);
        ball_body.velocity = Vector3.zero;
        yield return new WaitForSeconds(1f); // JOSH: Wait for 1 second before launching ball
        this.ball_body.AddForce(force.normalized * this.speed);
    }


    // MATT: handle collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // JOSH: Prevent ball from moving too horizontally
        float velocity = ball_body.velocity.magnitude;
        float maxXSpeed = velocity * (float)Math.Cos((minHorizontalAngle * (Math.PI)) / 180);

        if (Math.Abs(ball_body.velocity.x) > maxXSpeed)
        {
            int xDir = Math.Sign(ball_body.velocity.x); 
            int yDir = Math.Sign(ball_body.velocity.y);
            float minYSpeed = velocity * (float)Math.Sin((minHorizontalAngle * (Math.PI)) / 180);
            this.ball_body.velocity = new Vector3(xDir * maxXSpeed, yDir * minYSpeed, 0);
        }

        if (collision.gameObject.name == "Floor")
        {
            StartCoroutine("ResetBall");
            GameManager.Instance.LoseLife(); 
        }

        ball_animator.Play("bounce_1");

        if (collision.gameObject.name == "Brick")
        {
            screen_shake.BrickShake();
        }
        
        if (collision.gameObject.name == "Paddle")
        {
            screen_shake.PaddleShake();
        }
    }

}
