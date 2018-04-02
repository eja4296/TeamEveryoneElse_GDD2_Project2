using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useBoost : MonoBehaviour
{
    public GameObject robbie;
    Robber rob;
    float timer = 10;
    bool boost = false;

	// Use this for initialization
	void Start ()
    {
        robbie = GameObject.FindGameObjectWithTag("Player");
        rob = robbie.GetComponent<Robber>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(boost);

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (System.Int32.Parse(this.transform.Find("Text").GetComponent<Text>().text) > 0)
            {
                Boost();
                boost = true;
            }
        }

        if (boost)
        {
            if (timer >= 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    rob.movementSpeed = 450f;
                else
                    rob.movementSpeed = 275f;

                timer -= Time.deltaTime;
            }

            else if (timer <= 0)
                Destroy(this.gameObject);

            else
            {
                timer = 5;
                boost = false;
            }
        }
	}

    void Boost()
    {
        string temp = this.transform.Find("Text").GetComponent<Text>().text;

        if (System.Int32.Parse(temp) > 1)
        {
            int tcount = System.Int32.Parse(temp) - 1;
            this.transform.Find("Text").GetComponent<Text>().text = "" + tcount;
        }
        else
            Destroy(this.gameObject);
    }
}
