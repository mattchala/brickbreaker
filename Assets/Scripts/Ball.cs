using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D ball_body { get; private set; }
    public float speed = 1000f;
    private CameraShake screen_shake;


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
