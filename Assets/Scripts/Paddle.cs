using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Vector2 move_dir { get; private set; }
    public float move_speed = 50f;
    public float max_x_pos = 16f;
    private CameraShake screen_shake;


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


    // MATT: Handle collisions with paddle, consider doing this with left and right wall as well for triggers for sound and visual feedback
    // MATT: This is a unity built in
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            // MATT: can use this to trigger animations, particles, and sound effects when collides with ball
            //Debug.Log("BALL!");
        }

        // these 2 dont trigger
        if (collision.gameObject.name == "LeftWall")
        {
            screen_shake.LeftWallShake();
        }
    
        if (collision.gameObject.name == "RightWall")
        {
            screen_shake.RightWallShake();
        }
    }


    // MATT: Uses input to calculate directional vector with x being clamped between -1 and 1
    // MATT: avoids using else so certain branches don't take precedence if multiple direction keys are held
    private void CalcInputPaddleDiraction()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.move_dir += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.move_dir += Vector2.right;
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
}
