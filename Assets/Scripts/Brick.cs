using UnityEngine;
using UnityEngine.SceneManagement; 

public class Brick : MonoBehaviour
{
    public int brick_health = 2;
    public SpriteRenderer brick_sprite;
    public Color32 redish;
    //public Color32 orangeish;
    public Color32 greenish;
    public Color32[] brick_states;
    public Animator brick_animator;    // MATT: this creates an empty slot in unity editor, drag the desired animator there
    public ParticleSystem smoke;
    public ParticleSystem sparks;
    public ParticleSystem confetti;

    // MATT: Unity built-in 
    private void Awake()
    {
        // MATT: cash sprite renderer in brick sprite variable and fill brick states array with 3 colors, had to do it here in Awake since I couldn't just initialize them with the values
        this.brick_sprite = GetComponent<SpriteRenderer>();   
        this.redish = new Color32(251, 80, 80, 255);
        //this.orangeish = new Color32(255, 184, 54, 255);
        this.greenish = new Color32(139, 255, 131, 255);
        //this.brick_states = new Color32[3] { greenish, orangeish, redish };
        this.brick_states = new Color32[2] { greenish, redish };
    }


    // MATT: Unity built-in, sets the color on level load
    private void Start()
    {
        SetColor();
    }


    // MATT: Unity built-in, detects collisions with the ball
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.name == "Ball")
        {

            HandleHit();

        }    
    }


    // MATT: handles outcomes for detected collisions with the ball
    private void HandleHit()
    {
        // MATT: Add to points here
        this.brick_health--;
        if (this.brick_health <= 0)
        {
            // TODO play destroy sound
            EmitParticles();
            brick_animator.Play("brick_destroy");
            if (this.tag == "Player1")
            {
                ScoreManager.Instance.AddScore(1, 1);
            }
            if (this.tag == "Player2")
            {
                ScoreManager.Instance.AddScore(1, 2);
            }
        }
        else
        {
            brick_animator.Play("brick_hit");
            // TODO play collision sound
            SetColor();
        }

    }


    // MATT: this sets the brick color according to it's health. red is 3, orange is 2, green is 1 
    private void SetColor()
    {
        this.brick_sprite.color = this.brick_states[this.brick_health - 1];
    }

    // MATT: deactivates the brick, called using an Animation Event at the end of the 'brick_destroy' animation
    private void Deactivate()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.levelComplete();
    }

    // MATT: emits three different particle systems from brick's local 0 position
    private void EmitParticles()
    {
        smoke.Play();
        sparks.Play();
        confetti.Play();
    }

}
