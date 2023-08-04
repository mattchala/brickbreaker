using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Vector2 move_dir { get; private set; }
    public float move_speed = 50f;
    private float max_x_pos = 16f;
    private CameraShake screen_shake;
    public Animator paddle_animator;    // MATT: this creates an empty slot in unity editor, drag the desired animator there
    public ParticleSystem smoke;
    public ParticleSystem sparks;
    public ParticleSystem dust_left;
    public ParticleSystem dust_right;
    private string ball_collide_anim;
    private string wall_bump_anim;

    // MATT: Unity built-in 
    private void Start()
    {
        // Adjust width of player's paddle based on difficulty
        // MATT: sets the corresponding animations in variables and plays the initial idle anim relative to the size set by the difficulty level
        // MATT: adjusts max_x_pos to accomodate adjustment in paddle size
        if (this.tag == "Player1")
        {
            if (PlayerPrefs.GetFloat("PlayerDifficulty") == 0.0f)
            {
                // Easy Difficulty
                ball_collide_anim = "ball_collide_easy";
                wall_bump_anim = "wall_bump_easy";
                paddle_animator.Play("paddle_idle_easy");
                max_x_pos = 15.4f;
            }
            else if (PlayerPrefs.GetFloat("PlayerDifficulty") == 1.0f)
            {
                // Medium Difficulty
                ball_collide_anim = "ball_collide_medium";
                wall_bump_anim = "wall_bump_medium";
                paddle_animator.Play("paddle_idle_medium");
                max_x_pos = 15.7f;
            }
            else
            {
                // Hard Difficulty
                ball_collide_anim = "ball_collide";
                wall_bump_anim = "wall_bump";
                paddle_animator.Play("paddle_idle");
                max_x_pos = 16f;
            }
        }
        screen_shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }


    // MATT: Called every frame, not fixed, usually for getting user input
    // MATT: This is a unity built in
    private void Update()
    {
        CalcInputPaddleDiraction();
        MovePaddle();
    }


    // MATT: Handle collisions with paddle, consider doing this with left and right wall as well for triggers for sound and visual feedback like animations and particle effects
    // MATT: This is a unity built in
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            if (PlayerPrefs.GetFloat("AnimationsOn") == 1)
            {
                paddle_animator.Play(ball_collide_anim);
            }
        }

        if (collision.gameObject.name == "LeftWall")
        {
            if (PlayerPrefs.GetFloat("ScreenShakeOn") == 1 && this.tag == "Player1")
            {
                screen_shake.LeftWallShake();
            }
            if (PlayerPrefs.GetFloat("AnimationsOn") == 1)
            {
                paddle_animator.Play(wall_bump_anim);
            }
            if (PlayerPrefs.GetFloat("ParticlesOn") == 1)
            {
                EmitParticles(collision);
            }
        }
    
        if (collision.gameObject.name == "RightWall")
        {
            if (PlayerPrefs.GetFloat("ScreenShakeOn") == 1 && this.tag == "Player1")
            {
                screen_shake.RightWallShake();
            }
            if (PlayerPrefs.GetFloat("AnimationsOn") == 1)
            {
                paddle_animator.Play(wall_bump_anim);
            }
            if (PlayerPrefs.GetFloat("ParticlesOn") == 1)
            {
                EmitParticles(collision);
            }
        }
    }


    // MATT: Uses input to calculate directional vector with x being clamped between -1 and 1
    // MATT: avoids using else so certain branches don't take precedence if multiple direction keys are held
    private void CalcInputPaddleDiraction()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.move_dir += Vector2.left;
            
            if (PlayerPrefs.GetFloat("ParticlesOn") == 1)
            {
                dust_left.Play();
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.move_dir += Vector2.right;

            if (PlayerPrefs.GetFloat("ParticlesOn") == 1)
            {
                dust_right.Play();
            }
        }
        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) &&
            !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            this.move_dir = Vector2.zero;
        }
        this.move_dir = new Vector2(Mathf.Clamp(this.move_dir.x, -1, 1), 0);
    }
    

    // MATT: This moves the paddle using the calculated direction and speed
    private void MovePaddle()
    {
        if ((this.move_dir.x > 0 && transform.localPosition.x < max_x_pos) || (this.move_dir.x < 0 && transform.localPosition.x > -max_x_pos))
        {
            this.transform.localPosition += Vector3.right * this.move_dir.x * move_speed * Time.deltaTime;
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
