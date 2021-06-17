using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Twist = RosMessageTypes.Geometry.Twist;
public class VelSubscriber : Agent
{
    public Rigidbody rb;
    private float vel_x;
    private float vel_x_old=10f;
    private float vel_z;
    private float vel_z_old = 10f;
    public float index = 10f;
    void Start()
    {

       ROSConnection.instance.Subscribe<Twist>("cmd_vel", Vel_callback);
    }

    void Vel_callback(Twist data)
    {
        vel_x = (float)data.linear.x;
        vel_z = (float)data.linear.z;
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vel_x;
        controlSignal.z = vel_z;
        Debug.Log("Vel X:" + controlSignal.x);
        Debug.Log("Vel Z:" + controlSignal.z);
        vel_x_old = vel_x;
        vel_z_old = vel_z;
        rb.AddForce(controlSignal*index);

    }

    // private void FixedUpdate() 
    // {
    //     // if (vel_x == vel_x_old){ vel_x = 0;
    //     //                          vel_z = 0;}
        
    //     Vector3 controlSignal = Vector3.zero;
    //     controlSignal.x = vel_x;
    //     controlSignal.z = vel_z;
    //     vel_x_old = vel_x;
    //     vel_z_old = vel_z;
    //     Debug.Log("Vel X:" + controlSignal.x);
    //     Debug.Log("Vel Z:" + controlSignal.z);
    //     // var contiuousActionsOut = ActionBuffers.ContinuousActions;
    //     // contiuousActionsOut[0]  = vel_x;
    //     // contiuousActionsOut[1]  =vel_z;
    //     rb.AddForce(controlSignal*index);
    // }
    
    



}