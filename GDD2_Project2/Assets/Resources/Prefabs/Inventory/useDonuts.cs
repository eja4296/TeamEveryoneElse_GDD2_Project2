using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useDonuts : MonoBehaviour
{
    public GameObject donuts;
    GameObject clone;
    public GameObject rob;
    public bool instaniated = false;
    float duration = 5;

    // Use this for initialization
    void Start()
    {
        rob = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (System.Int32.Parse(this.transform.Find("Text").GetComponent<Text>().text) > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                clone = (GameObject)Instantiate(donuts, rob.transform.position, Quaternion.identity);
                instaniated = true;
            }
        }

        if (instaniated)
        {
            duration -= Time.deltaTime;

            if (duration <= 0)
            {
                Destroy(clone);
                duration = 5;
                instaniated = false;
                Donuts();
            }
        }

        Debug.Log(duration);
    }

    void Donuts()
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
