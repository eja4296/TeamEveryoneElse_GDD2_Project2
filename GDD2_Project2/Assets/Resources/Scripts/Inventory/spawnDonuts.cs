using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnDonuts : MonoBehaviour
{
    public GameObject donuts;

    void Spawn()
    {
        Vector3 donutsPosition = this.transform.position;

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
