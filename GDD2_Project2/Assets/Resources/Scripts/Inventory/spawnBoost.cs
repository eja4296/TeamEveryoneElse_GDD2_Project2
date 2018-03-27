using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoost : MonoBehaviour
{
    public GameObject boost;

    void Spawn()
    {
        Vector3 boostPosition = this.transform.position;

        Instantiate(boost, boostPosition, Quaternion.identity);
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
