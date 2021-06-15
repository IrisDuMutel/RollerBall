using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{

    public Rigidbody rBody;
    public Transform Target;
    public float index = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        
    }

    public override void OnEpisodeBegin()
    {
        // If the agent fell, zero its momentum
        if (this.transform.localPosition.y<0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0,0.5f,0);
        }

        // Move the target to a new spot
        Target.localPosition = new Vector3(Random.value*8-4,
                                            0.5f,
                                            Random.value*8-4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and angent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal*index);

        float distanceToTarget =  Vector3.Distance(this.transform.localPosition,Target.localPosition);
        // Reached target
        if (distanceToTarget<1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell off of the platf
        else if (this.transform.localPosition.y<0)
        {
            EndEpisode();
        }
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var contiuousActionsOut = actionsOut.ContinuousActions;
        contiuousActionsOut[0]  = Input.GetAxis("Horizontal");
        contiuousActionsOut[1]  = Input.GetAxis("Vertical");
    }

  
}
