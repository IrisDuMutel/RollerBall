using RosMessageTypes.RoboticsDemo;
using testmsg = RosMessageTypes.RoboticsDemo.TestMsg;
using UnityEngine;
using Random = UnityEngine.Random;
using target = RosMessageTypes.Geometry.Twist;

/// <summary>
/// 
/// </summary>
public class PosPublisher : MonoBehaviour
{
    public Transform Target_cube;
    public Transform Agent_ball;
    public Rigidbody AgentRb;
    ROSConnection ros;
    public string topicName = "BallOdometry";


    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.instance;
        
    }

    private void FixedUpdate()
    {

        float[] cube_pos = new float[]{Target_cube.localPosition.x,Target_cube.localPosition.y,Target_cube.localPosition.z};
        // cube_pos[1] = Target_cube.localPosition.y;
        // cube_pos[2] = Target_cube.localPosition.z;

        float[] ball_pos = new float[]{Agent_ball.localPosition.x,Agent_ball.localPosition.y,Agent_ball.localPosition.z};

        // ball_pos[0] = Agent_ball.localPosition.x;
        // ball_pos[1] = Agent_ball.localPosition.y;
        // ball_pos[2] = Agent_ball.localPosition.z;

        float[] ball_vel = new float[]{AgentRb.velocity.x,AgentRb.velocity.z};

        // ball_vel[0] = AgentRb.velocity.x;
        // ball_vel[1] = AgentRb.velocity.z;

        // if (timeElapsed > publishMessageFrequency)
        // {   
            

        TestMsg odomet = new TestMsg(cube_pos,  
                                ball_pos,
                                ball_vel);
        // Finally send the message to server_endpoint.py running in ROS
        ros.Send(topicName, odomet);

            
        // }
    }
}

