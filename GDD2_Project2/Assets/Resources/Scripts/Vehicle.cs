using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float radius = 1;
    public GameObject target;   // the target we are looking for
    public float maxSpeed;      // max speed of the object
    public float mass;          // mass will influence how fast the object will accelerate - basically how "heavy" it is
    Vector3 acceleration;       // how fast it takes to speed up
    public Vector3 velocity;    // current speed
    Vector3 position;           // position of the current object
    Vector3 steer;              // the direction we turn to move towards the target

	// Use this for initialization
	void Start ()
    {
        acceleration = Vector3.zero;        //does not move at the start 
        velocity = Vector3.zero;            // does not move at the start
        position = this.transform.position; //the position of the current object
	}
	
	// Update is called once per frame
	void Update ()
    {


        this.transform.rotation = Quaternion.LookRotation(this.velocity);   //makes the object rotate like a normal person
	}

    /// <summary>
    /// Apply a force to this object this frame,
    /// taking into account its mass
    /// </summary>
    /// <param name="force"></param>
    public void ApplyForce(Vector3 force)
    {
        this.acceleration += force / this.mass;         
    }

    /// <summary>
    /// Internally handles the overall movement based on acceleration, velocity, etc.
    /// </summary>
    public void Movement()
    {
        // Apply acceleration
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        // Throw the position back to Unity
        this.transform.position = position;

        // We're done with forces for this frame
        acceleration = Vector3.zero;
    }

    /// <summary>
    /// Calculate a steering force such that it allows us to seek a target position
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns>The steering force to get to the target</returns>
    public Vector3 Seek(Vector3 targetPosition)
    {
        // Calculate our "perfect" desired velocity
        Vector3 desiredVelocity = targetPosition - this.transform.position;

        // Limit desired velocity by max speed;
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        // Turning to move towards the desired velocity
        Vector3 steeringForce = desiredVelocity - this.velocity;
        return steeringForce;
    }

    /// <summary>
    /// Calculate the fleeing vector that will make the object run away from whatever it's running away from
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns>The steering force to get away from the target</returns>
    public Vector3 Flee(Vector3 targetPosition)
    {
        // Calculate our "imperfect" desired velocity
        Vector3 desiredVelocity = -targetPosition + this.transform.position;

        // Limit desired velocity by max speed
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        // How do we turn to start moving towards
        // the desired velocity?
        Vector3 steeringForce = desiredVelocity - this.velocity;
        return steeringForce;
    }

    /// <summary>
    /// Calculate the vector to avoid a circular obstacle. 
    /// </summary>
    /// <param name="script"></param>
    /// <param name="safeDistance"></param>
    /// <returns>Returns a vector to turn it left or right depending on it's position. Returns a zero vector if nothing is in front of the object</returns>
    public Vector3 AvoidObstacle(obstacleScript script, float safeDistance)     // safe distance refers to the amount of distance before the object will try to avoid the obstacle
    {
        Vector3 vecToObj = script.transform.position - this.transform.position;

        float projectedDist = Vector3.Dot(vecToObj, this.transform.right);

        if (vecToObj.magnitude < safeDistance && (Vector3.Dot(vecToObj, this.transform.forward) > 0) && Mathf.Abs(projectedDist) < (script.radius + this.radius))   // Circle Collision check
        {
            float weight = safeDistance / vecToObj.magnitude;

            if (projectedDist < 0)      //turn left
                return Seek(this.transform.right * 1500 * weight);
            else                        //turn right
                return Seek(-this.transform.right * 1500 * weight);
        }
        else
            return Vector3.zero;
    }

    /// <summary>
    /// Calculate the vector of the future position based on time of the targeted object.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="vel"></param>
    /// <returns>Returns a steering force of the targeted object's future position</returns>
    public Vector3 Pursue(Vector3 pos, Vector3 vel)
    {
        Vector3 futurePos = pos + vel * Time.deltaTime;

        return Seek(futurePos);
    }

    /// <summary>
    /// Calculate the vector of the anticapted position of the chaser and will "predict" by running away to a future position.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="vel"></param>
    /// <returns>Returns a steering force of the object's future fleeing position</returns>
    public Vector3 Evade(Vector3 pos, Vector3 vel)
    {
        Vector3 futurePos = pos + vel * Time.deltaTime;

        return Flee(futurePos);
    }
}
