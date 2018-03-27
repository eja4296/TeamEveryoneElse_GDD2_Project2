﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRandomPowerUp : MonoBehaviour
{
    public spawnBoost boost;
    public spawnDisguise disguise;
    public spawnDonuts donuts;

	// Use this for initialization
	void Start ()
    {
        boost = GetComponent<spawnBoost>();
        disguise = GetComponent<spawnDisguise>();
        donuts = GetComponent<spawnDonuts>();

        SpawnRandomPowerUp();
	}

    void SpawnRandomPowerUp()
    {
        int rand = Random.Range(1, 3);

        if (rand == 1)
        {
            boost.enabled = true;
            disguise.enabled = false;
            donuts.enabled = false;
            gameObject.tag = "boost";
        }
        if (rand == 2)
        {
            disguise.enabled = true;
            boost.enabled = false;
            donuts.enabled = false;
            gameObject.tag = "disguise";
        }
        if (rand == 3)
        {
            disguise.enabled = false;
            boost.enabled = false;
            donuts.enabled = true;
            gameObject.tag = "donut";
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
