using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float obstacle_speed;   // MATT: This is the speed of the obstacle movement
    public int starting_position;  // MATT: This is the starting position of the obstacle, choose the index
    public Transform[] positions;  // MATT: This is the array of transforms, you can add multpile and add drag them in in the editor
    private int positions_index;   // MATT: This privately tracks the current destination position of the obstacle

    // MATT: Unity Built-in, called before the first frame update
    void Start()
    {
        // MATT: Set position of obstacle to selected starting position
        this.transform.position = positions[starting_position].position;
    }

    // MATT: Unity Built-in, called once per frame
    void Update()
    {
        // MATT: Check if the obstacle is close to (basically touching) it's current destination position
        if (Vector2.Distance(this.transform.position, positions[positions_index].position) < 0.02f)
        {
            // MATT: Change destination position to next position in list or reset if at the end
            positions_index += 1;
            if (positions_index == positions.Length)
            {
                positions_index = 0;
            }
        }
        // Move towards the current destination position
        this.transform.position = Vector2.MoveTowards(this.transform.position, positions[positions_index].position, obstacle_speed * Time.deltaTime);
    }
}
