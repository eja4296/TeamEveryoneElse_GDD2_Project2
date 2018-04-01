using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useDisguise : MonoBehaviour
{
    public GameObject robber;
    float time = 10;
    SkinnedMeshRenderer m;
    public bool disguise = false;
    float count = 0;

	// Use this for initialization
	void Start ()
    {
        robber = GameObject.FindGameObjectWithTag("mesh");
        m = robber.GetComponent<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (System.Int32.Parse(this.transform.Find("Text").GetComponent<Text>().text) > 0)
            {
                Disguise();
                disguise = true;
            }
        }

        if (disguise)
        {
            if (time >= 0)
            {
                toggleMaterial(m);

                time -= Time.deltaTime;
            }
            else if (time <= 0)
            {
                toggleMaterial(m);
                Destroy(this.gameObject);
            }
            else
            {
                toggleMaterial(m);
                time = 10;
                disguise = false;
            }
        }
	}

    void toggleMaterial(SkinnedMeshRenderer m)
    {
        Material cop = Resources.Load("Models/Materials/Cop", typeof(Material)) as Material;
        Material robber = Resources.Load("Materials/Robber", typeof(Material)) as Material;

        if (time >= 0)
            m.material = cop;
        else
            m.material = robber;
    }

    void Disguise()
    {
        if (System.Int32.Parse(this.transform.Find("Text").GetComponent<Text>().text) > 1)
        {
            int tcount = System.Int32.Parse(this.transform.Find("Text").GetComponent<Text>().text) - 1;
            this.transform.Find("Text").GetComponent<Text>().text = "" + tcount;
        }
    }
}
