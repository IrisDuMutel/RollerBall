using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Twist = RosMessageTypes.Geometry.Twist;

public class RollerAgent : Agent
{

    public Rigidbody rBody;
    private float vel_x =0.0f;
    private float vel_x_old=10f;
    private float vel_z = 0.0f;
    private float vel_z_old = 10f;
    public Transform Target;
    public float index = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        ROSConnection.instance.Subscribe<Twist>("cmd_vel", Vel_callback);
        
    }
    void Vel_callback(Twist data)
    {
        vel_x = (float)data.linear.x;
        vel_z = (float)data.linear.z;
        // Vector3 controlSignal = Vector3.zero;
        // controlSignal.x = vel_x;
        // controlSignal.z = vel_z;
        // vel_x_old = vel_x;
        // vel_z_old = vel_z;
        // rb.AddForce(controlSignal*index);
        Debug.Log("Inside Callback " + Time.fixedDeltaTime);

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


        // TESTING
        // sensor.AddObservation(new Vector3(1f,0.5f,1f));
        // sensor.AddObservation(new Vector3(0.0f,0.5f,0.0f));
        // sensor.AddObservation((0.0f));
        // sensor.AddObservation((0.0f));


    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        // controlSignal.x = vel_x;
        // controlSignal.z = vel_z;
        // Debug.Log("Vel X:" + controlSignal.x);
        // Debug.Log("Vel Z:" + controlSignal.z);
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
        // contiuousActionsOut[0]  = vel_x;
        // contiuousActionsOut[1]  = vel_z;
    }

  
}
