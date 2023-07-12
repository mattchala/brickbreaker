using UnityEngine;
using System;


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
        Invoke(nameof(SetTrajectory), 1f); 
    }


    // MATT: sets the ball's initial direction in a random angle, but always downward
    private void SetTrajectory()
    {
        Vector2 force = Vector2.zero;
        //force.x = Random.Range(-1f, 1f);  // MATT: Comment this line out if you don't want the starting ball trajectory to be randomized
        force.y = -1f;
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
