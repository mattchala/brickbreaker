using UnityEngine;
using System;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Rigidbody2D ball_body { get; private set; }
    public GameObject paddle;
    public float speed = 20f;
    private CameraShake screen_shake;
    public Animator ball_animator;    // MATT: this creates an empty slot in unity editor, drag the desired animator there
    public int minHorizontalAngle = 25; // MATT: was 5, trying higher val
    private bool ball_is_moving = false;
    public ParticleSystem smoke;
    public ParticleSystem sparks;

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

        // Adjust speed of player's ball based on difficulty
        if (this.tag == "Player1")
        {
            if (PlayerPrefs.GetFloat("PlayerDifficulty") == 0.0f)
            {
                speed *= 0.8f; // JOSH: Speed is 80% on easy difficulty
            }
            if (PlayerPrefs.GetFloat("PlayerDifficulty") == 1.0f)
            {
                speed *= 0.9f; // JOSH: Speed is 90% on medium difficulty
            }
        }

        // MATT: instead of immediately calling the set_trajectory function, we wait 1 second and then fire it off
        StartCoroutine("ResetBall");
    }


    // MATT: Unity built-in
    private void Update()
    {
        // MATT: check every frame and clamp velocity to speed, this fixed the issue in an error-reproduction level i created
        ClampVelocity();
    }


    // MATT: handle collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // JOSH: Prevent ball from moving too horizontally
        CalculateMaxAngle();

        if (collision.gameObject.name == "Floor")
        {
            paddle.transform.localPosition = new Vector3(0, paddle.transform.localPosition.y, paddle.transform.localPosition.z);
            StartCoroutine("ResetBall");


            // Debug.Log("LOST BALL");
            PaddleAgent.Instance.AddReward(-10f);
            PaddleAgent.Instance.EndEpisode();

            if (this.tag == "Player1")
            {
                GameManager.Instance.LosePlayerLife(); 
            }
            if (this.tag == "Player2")
            {
                GameManager.Instance.LoseAILife(); 
            }
        }

        if (PlayerPrefs.GetFloat("AnimationsOn") == 1)
        {
            ball_animator.Play("bounce_1");
        }

        if (PlayerPrefs.GetFloat("ParticlesOn") == 1)
        {
            EmitParticles(collision);
        }

        if (this.tag == "Player1") // JOSH: Disable screenshake for player 2
        {
            if (PlayerPrefs.GetFloat("ScreenShakeOn") == 1 && collision.gameObject.name == "Brick")
            {
                screen_shake.BrickShake();
            }
            
            if (PlayerPrefs.GetFloat("ScreenShakeOn") == 1 && collision.gameObject.name == "Paddle")
            {
                screen_shake.PaddleShake();
            }
        }
    }


    // MATT: sets the ball's initial direction in a random angle, but always downward
    private IEnumerator ResetBall()
    {
        ball_is_moving = false;
        Vector2 force = Vector2.zero;
        force.x = UnityEngine.Random.Range(-1f, 1f);  // MATT: Comment this line out if you don't want the starting ball trajectory to be randomized
        force.y = -1f;
        transform.localPosition = new Vector3(0, 0, 0);
        ball_body.velocity = Vector3.zero;
        yield return new WaitForSeconds(1f); // JOSH: Wait for 1 second before launching ball
        this.ball_body.AddForce(force.normalized * this.speed);
        yield return new WaitForSeconds(.1f); // MATT: waits for velocity.y to not equal 0 before setting ball_is_moving to true 
        ball_is_moving = true;

    }


    private void CalculateMaxAngle()
    {
        ball_is_moving = false; // MATT: stops Update function modifying the velocity
        float velocity = ball_body.velocity.magnitude;
        float maxXSpeed = velocity * (float)Math.Cos((minHorizontalAngle * (Math.PI)) / 180);

        if (Math.Abs(ball_body.velocity.x) > maxXSpeed)
        {
            int xDir = Math.Sign(ball_body.velocity.x); 
            int yDir = Math.Sign(ball_body.velocity.y);
            float minYSpeed = velocity * (float)Math.Sin((minHorizontalAngle * (Math.PI)) / 180);
            this.ball_body.velocity = new Vector3(xDir * maxXSpeed, yDir * minYSpeed, 0);
        }
        ball_is_moving = true; // MATTL starts Update function modifying the velocity
    }


    // MATT: clamps the velocity of the ball to avoid the occasional slow down bug when the ball rapidly bounces
    private void ClampVelocity()
    {
        if (ball_is_moving)
        {
            if (this.ball_body.velocity.y == 0)
            {
                Vector3 vel = this.ball_body.velocity;
                vel.y = 1;  // MATT: set a 1 instead of -1 so will go upward to give player a chance to react
                this.ball_body.velocity = vel;
            }
            this.ball_body.velocity = this.ball_body.velocity.normalized * speed;
        }
    }

    // MATT: sets the position of the child particles to the position of the the point of contact and them emits them
    private void EmitParticles(Collision2D collision)
    {
        smoke.transform.position = collision.GetContact(0).point;
        sparks.transform.position = collision.GetContact(0).point;
        smoke.Play();
        sparks.Play();
    }

}
