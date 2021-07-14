using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RosMessageTypes.RoboticsDemo;
using Twist = RosMessageTypes.Geometry.Twist;


public class SceneReset : MonoBehaviour {

    public Rigidbody ballRb;
    public Transform ballTf;
    public Transform cubeTf;
    private float[] ball_pos;
    private float[] cube_pos;
    private float[] ball_vel;
    private bool new_ep;
    public string topicName = "ResetScene";
    void Start()
    {

       ROSConnection.instance.Subscribe<OdometryMsg>(topicName, Reset_callback);
    }

    void Reset_callback(OdometryMsg data)
    {
        ball_pos = data.ball_pos;
        ball_vel = data.ball_vel;
        cube_pos = data.cube_pos;
        new_ep = data.new_ep;

        if (new_ep== true){
        
            ballTf.position = new Vector3(ball_pos[0],ball_pos[1],ball_pos[2]);
            cubeTf.position = new Vector3(cube_pos[0],cube_pos[1],cube_pos[2]);
            ballRb.velocity = new Vector3(ball_vel[0],0.0f,ball_vel[1]);
        }

    }


    
    



}