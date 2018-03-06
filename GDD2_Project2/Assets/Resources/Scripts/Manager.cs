using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Attributes for the manager
    GameObject[] obstacle;
    obstacleScript[] buildings;

    //list of all vehicle scripts - vehicle is basically things that move
    Vector3 steer;
    List<Vehicle> all = new List<Vehicle>();

    //robber attributes
    public GameObject robber;
    Robber rob;

    //list of cop scripts
    public List<GameObject> cops;
    List<CopScript> copScripts;

    // attributes for AI
    float distance;
    float temp;     // the range of "vision" for the cop

	// Use this for initialization
	void Start ()
    {
        rob = robber.GetComponent<Robber>();      // find robber script
        copScripts = new List<CopScript>();
        
        for (int i = 0; i < cops.Count; i++)
        {
            copScripts.Add(cops[i].GetComponent<CopScript>());
            all.Add(cops[i].GetComponent<Vehicle>());             // adding all cops to the list of vehicles
        }

        obstacle = GameObject.FindGameObjectsWithTag("obstacle");
        buildings = new obstacleScript[obstacle.Length];

        for (int i = 0; i < obstacle.Length; i++)
            buildings[i] = obstacle[i].GetComponent<obstacleScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        temp = 50;

        for (int i = 0; i < copScripts.Count; i++)
        {
            if (Vector3.Distance(rob.transform.position, copScripts[i].transform.position) < temp)       //check to see if within range
            {
                temp = Vector3.Distance(rob.transform.position, copScripts[i].transform.position);
                copScripts[i].seekTarget = robber;
            }

            if (Vector3.Distance(rob.transform.position, copScripts[i].transform.position) < 3)      // essentially if the cop is literally on the robber, kill the robber
            {
                //death of some sort
            }
        }

        ///Calculate vectors for the cops to avoid obstacles - not needed for player because the player is responsible for doing that
        foreach (Vehicle v in all)
        {
            for (int i = 0; i < buildings.Length; i++)      
            {
                Vector3 avoidForce = v.AvoidObstacle(buildings[i], 4.5f);
                avoidForce.y = 0;
                v.ApplyForce(avoidForce);
            }
        }

        /*
         *                                                                                                       *This commented code will make the cops just wander. 
         * if (robber is dead)                                                                                   *The giant if statement with the 50s in it checks to make sure that the cops
         * {                                                                                                      are within the bounds of the map, so the 50s are just a placeholder value for now.
         *      for (int i = 0; i < copScripts.Count; i++)
         *      {
         *          Vehicle check = copScripts[i].GetComponent<Vehicle>();
         *          Wander(check);
         *          
         *          if (copScripts[i].transform.position.x > 50 || copScripts[i].transform.position.x < -50 || copScripts[i].transform.position.z < -50 || copScripts[i].transform.position.z > 50)
         *          {
         *              steer = check.Seek(new Vector3(0, 0.5f, 0);
         *              steery.y = 0;
         *              check.ApplyForce(steer);
         *          }
         *      }
         * }
         * 
         * public void Wander (Vehicle v)                                                   *This is the method for the AI to simply wander
         * {
         *      float strength = Random.Range(7.5f, 30);
         *  
         *      Vector2 offset2 = Random.insideUnitCircle;
         *      Vector3 offset3 = new Vector3(offset2.x, 0, offset2.y);
         *      offset3.Normalize();
         *      offset3 *= strength;
         *      
         *      v.ApplyForce(offset);
         * }
         */
    }
}
