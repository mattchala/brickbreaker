using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PaddleAgent : Agent
{
    public static PaddleAgent Instance;
    public Ball ball;

    public Transform[] obstacles;     // MATT: make a modifiable list in UI to add obstacles to track and add

    private void Awake()
    {
        Instance = this;
    }

    public float max_x_pos = 16f;
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        // float moveSpeed = 50f;
        float max_x_pos = 16f;

        Instance.AddReward(0.1f);
        // only move left or right if not exceeding that boundary
        if ((moveX > 0 && transform.localPosition.x < max_x_pos) || (moveX < 0 && transform.localPosition.x > -max_x_pos))
        {
            transform.localPosition += new Vector3(moveX, 0, 0);
        }
    }

    [SerializeField] private Transform targetTransform;
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(targetTransform.localPosition.x);
        sensor.AddObservation(targetTransform.localPosition.y);
        sensor.AddObservation(ball.ball_body.velocity);
        
        // velocity direction  
        sensor.AddObservation((targetTransform.position - transform.position)/(targetTransform.position - transform.position).magnitude);
        
        // distance from ball to paddle
        sensor.AddObservation(Vector3.Distance (transform.position, targetTransform.position));


        // MATT: add the current obstacle positions as observations if they exist
        foreach(var obstacle in obstacles) 
        {
            sensor.AddObservation(obstacle.localPosition);
        }
        
        // distance from center of paddle to center of left wall
        // sensor.AddObservation((float)Math.Abs(transform.position.x - -16));

        // distance from center of paddle to center of right wall
        // sensor.AddObservation((float)Math.Abs(transform.position.x - 16));

        // Debug.Log("PADDLE " + transform.position.x.ToString());
        // Debug.Log("BALL " + targetTransform.position.ToString());
        // Debug.Log(Vector3.Distance (transform.position, targetTransform.position));
        // Debug.Log("BALL" + targetTransform.position.x.ToString() + " " + targetTransform.position.y.ToString());
        // Debug.Log("LEFT " + Math.Abs(transform.position.x - -16.69665).ToString());
        // Debug.Log("RIGHT " + Math.Abs(transform.position.x - 19.85335).ToString());
    }
    

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, -8, 0);
        targetTransform.localPosition = Vector3.zero;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        // continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.name == "Ball")
        {
            Instance.AddReward(+1f);
            Debug.Log("BALL HIT");
        }
    }
    private void OnTriggerEnter(Collider other){


        if (other.TryGetComponent<Floor>(out Floor floor))
        {
            SetReward(-1f);
            Debug.Log("Loss");
            EndEpisode();
        }

    }
}
