using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnDonuts : MonoBehaviour
{
    public GameObject donuts;
    public GameObject trashcan;
    MeshRenderer m;

    void Spawn()
    {
        trashcan = GameObject.FindGameObjectWithTag("trashcan");
        m = donuts.GetComponent<MeshRenderer>();
        m.enabled = false;

        Vector3 donutsPosition = trashcan.transform.position;

        Instantiate(donuts, donutsPosition, Quaternion.identity);
    }

    // Use this for initialization
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
