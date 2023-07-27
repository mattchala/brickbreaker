using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PaddleAgent : Agent
{
    public float max_x_pos = 16f;
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];

        float moveSpeed = 1f;
        transform.position += new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;
    }

    [SerializeField] private Transform targetTransform;
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(0, -8, 0);
        targetTransform.position = Vector3.zero;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.rigidbody.name == "Ball")
        {
            SetReward(+1f);
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
