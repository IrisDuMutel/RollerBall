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

    // private Vector3 cube_pos;
    // private float[] ball_pos;
    // private float[] cube_pos;
    private float[] ball_vel;
    // private Vector3 ball_pos;
    // private Vector2 ball_vel;

    ROSConnection ros;
    public string topicName = "odometry";
    public float publishMessageFrequency = 0.5f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.instance;
        
    }

    private void FixedUpdate()
    {
        // timeElapsed += Time.deltaTime;

        float[] cube_pos = new float[]{Target_cube.position.x,Target_cube.position.y,Target_cube.position.z};
        // cube_pos[1] = Target_cube.position.y;
        // cube_pos[2] = Target_cube.position.z;

        float[] ball_pos = new float[]{Agent_ball.position.x,Agent_ball.position.y,Agent_ball.position.z};

        // ball_pos[0] = Agent_ball.position.x;
        // ball_pos[1] = Agent_ball.position.y;
        // ball_pos[2] = Agent_ball.position.z;

        float[] ball_vel = new float[]{AgentRb.velocity.x,AgentRb.velocity.z};


        // ball_vel[0] = AgentRb.velocity.x;
        // ball_vel[1] = AgentRb.velocity.z;


        // RosMessageTypes.Geometry.Vector3 cube_pos = new RosMessageTypes.Geometry.Vector3(Target_cube.position.x,
        //                                                                                 Target_cube.position.y,
        //                                                                                 Target_cube.position.z);
                                                                                        
        // RosMessageTypes.Geometry.Vector3 ball_pos = new RosMessageTypes.Geometry.Vector3(Agent_ball.position.x,
        //                                                                                 Agent_ball.position.y,
        //                                                                                 Agent_ball.position.z);
        // RosMessageTypes.Geometry.Vector3 ball_vel = new RosMessageTypes.Geometry.Vector3(AgentRb.velocity.x,AgentRb.velocity.z,AgentRb.velocity.y);

        // if (timeElapsed > publishMessageFrequency)
        // {   
            

            TestMsg odomet = new TestMsg(cube_pos,  
                                    ball_pos,
                                    ball_vel);

            // Finally send the message to server_endpoint.py running in ROS
            ros.Send(topicName, odomet);

            timeElapsed = 0;
        // }
    }
}

