using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Vector2 move_dir { get; private set; }
    public float move_speed = 50f;
    public float max_x_pos = 16f;
    private CameraShake screen_shake;
    public Animator paddle_animator;    // MATT: this creates an empty slot in unity editor, drag the desired animator there
    public ParticleSystem smoke;
    public ParticleSystem sparks;
    public ParticleSystem dust_left;
    public ParticleSystem dust_right;

    // MATT: Unity built-in 
    private void Start()
    {
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
                paddle_animator.Play("ball_collide");
            }
        }

        if (collision.gameObject.name == "LeftWall")
        {
            EmitParticles(collision);
            if (PlayerPrefs.GetFloat("ScreenShakeOn") == 1)
            {
                screen_shake.LeftWallShake();
            }
            if (PlayerPrefs.GetFloat("AnimationsOn") == 1)
            {
                paddle_animator.Play("wall_bump");
            }
        }
    
        if (collision.gameObject.name == "RightWall")
        {
            EmitParticles(collision);
            if (PlayerPrefs.GetFloat("ScreenShakeOn") == 1)
            {
                screen_shake.RightWallShake();
            }
            if (PlayerPrefs.GetFloat("AnimationsOn") == 1)
            {
                paddle_animator.Play("wall_bump");
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
            dust_left.Play();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.move_dir += Vector2.right;
            dust_right.Play();
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
        if ((this.move_dir.x > 0 && transform.position.x < max_x_pos) || (this.move_dir.x < 0 && transform.position.x > -max_x_pos))
        {
            this.transform.position += Vector3.right * this.move_dir.x * move_speed * Time.deltaTime;
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
