using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D ball_body { get; private set; }
    public float speed = 1000f;

    // MATT: Unity built-in 
    private void Awake()
    {
        // MATT: Cache objects into variables on load here
        this.ball_body = GetComponent<Rigidbody2D>();
    }


    // MATT: Unity built-in 
    private void Start()
    {
        // MATT: instead of immediately calling the set_trajectory function, we wait 1 second and then fire it off
        Invoke(nameof(SetTrajectory), 1f); 
    }


    // MATT: Unity built-in 
    // MATT: Called by Unity every frame
    private void Update()
    {
        MaintainSpeed();
    }


    // MATT: sets the ball's initial direction in a random angle, but always downward
    private void SetTrajectory()
    {
        Vector2 force = Vector2.zero;
        //force.x = Random.Range(-1f, 1f);  // MATT: Comment this line out if you don't want the starting ball trajectory to be randomized
        force.y = -1f;
        this.ball_body.AddForce(force.normalized * this.speed);
    }


    // MATT: clamps ball's movement speed
    // MATT: i think it works...but im not sure. if the ball slows down at some point, then it doesn't work
    private void MaintainSpeed()
    {
        this.ball_body.velocity = Vector3.ClampMagnitude(this.ball_body.velocity, speed);
    }
}
