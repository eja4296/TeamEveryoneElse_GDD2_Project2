using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoost : MonoBehaviour
{
    public GameObject boost;
    public GameObject trashcan;
    MeshRenderer m;

    void Spawn()
    {
        trashcan = GameObject.FindGameObjectWithTag("trashcan");
        m = boost.GetComponent<MeshRenderer>();
        m.enabled = false;

        Vector3 boostPosition = trashcan.transform.position;

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
