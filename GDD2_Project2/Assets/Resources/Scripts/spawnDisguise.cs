using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnDisguise : MonoBehaviour
{
    public GameObject disguise;
    public GameObject trashcan;
    Renderer m;

    void Spawn()
    {
        trashcan = GameObject.FindGameObjectWithTag("trashcan");
        m = disguise.GetComponent<Renderer>();
        m.enabled = false;

        Vector3 disguisePosition = trashcan.transform.position;

        Instantiate(disguise, disguisePosition, Quaternion.identity);
    }

    void toggleMaterial(Renderer m)
    {
        float time = 5;

        Material cop = Resources.Load("Models/Materials/Cop", typeof(Material)) as Material;
        Material robber = Resources.Load("Materials/Robber", typeof(Material)) as Material;

        if (time > 0)
            m.material = cop;
        else
            m.material = robber;

        time -= Time.deltaTime;
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
