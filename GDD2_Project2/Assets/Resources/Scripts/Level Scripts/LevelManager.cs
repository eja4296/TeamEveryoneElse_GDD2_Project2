﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LevelManager : MonoBehaviour {
    //list of all building prefabs
    public List<GameObject> buildings;
    //cop prefab
    public GameObject cop;

    //stores player location
    public Transform player;
    //stores level transform to parent new level elements
    public Transform level;

    //percentage from 0-100 of how often plots spawn completely empty
    public float emptyPlotPercentage;
    //percentage chance of spawning on each waypoint. THERE ARE 37 WAYPOINTS PER PLOT
    public float spawnPercentage;
    //2d list of levelmap
    List<List<GameObject>> levelMap;
    public GameObject dumpster;
    public GameObject trashCan;
    public GameObject block;
    public GameObject car;

    //Y axis in vector2 is measurement of Z in game
    public Vector2 blockSize;

    //the number of chunks the level should be generated to in the X and the Y
    public int renderDist;
    int oldX, oldY;
    public int playerX, playerY;

    public Vector2 PlotSize;

    //increasing difficulty
    public float difficultyIncreaseTime;
    float difficultyTimer;

    // Use this for initialization
    void Start() {
        difficultyTimer = difficultyIncreaseTime;
        Cursor.lockState = CursorLockMode.Locked;   
        //initalize all buildings
        buildings = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Level Prefabs/Buildings"));
        
        //load prefabs
        block = Resources.Load<GameObject>("Prefabs/Level Prefabs/block");
        cop = Resources.Load<GameObject>("Prefabs/Cop");
        
        //initialize levelmap list
        levelMap = new List<List<GameObject>>();
        while (levelMap.Count < renderDist * 2) {
            levelMap.Add(new List<GameObject>());
        }
        for (int x = 0; x < levelMap.Count; x++) {
            for (int y = 0; y < renderDist * 2; y++) {
                GameObject newChunk = Instantiate<GameObject>(block, level);
                newChunk.transform.parent = gameObject.transform;
                levelMap[x].Add(newChunk);
                //position it around player
                newChunk.transform.position = new Vector3((renderDist * blockSize.x * -1) + (x * blockSize.x), 0f, (renderDist * blockSize.y * -1) + (y * blockSize.y));
                FillPlots(newChunk);
            }
        }
        playerX = (int)((player.transform.position.x + ((renderDist + .5f) * blockSize.x)) / blockSize.x);
        playerY = (int)((player.transform.position.z + ((renderDist + .5f) * blockSize.y)) / blockSize.y);
        oldX = playerX;
        oldY = playerY;
    }
    public void FillPlots(GameObject newChunk)
    {
        List<GameObject> plots = new List<GameObject>();
        List<GameObject> carSpawns = new List<GameObject>();
        List<GameObject> garbageSpawns = new List<GameObject>();
        List<GameObject> dumpsterSpawns = new List<GameObject>();

        GameObject plotObj = newChunk.transform.Find("Plots").gameObject;
        GameObject carObj = plotObj.transform.Find("Cars").gameObject;
        GameObject dumpsterObj = plotObj.transform.Find("DumpsterSpawns").gameObject;
        GameObject garbageObj = plotObj.transform.Find("GarbageSpawns").gameObject;

        
        for(int i = 1; i <= 30; i++) {
            carSpawns.Add(carObj.transform.Find("Car" + i).gameObject);
        }
        foreach(GameObject o in carSpawns) {
            if(Random.Range(0.0f, 1f) < .25f) {
                GameObject newCar = Instantiate(car);
                newCar.transform.parent = o.transform;
                newCar.transform.position = o.transform.position + new Vector3(0f, 1f, 0f);
                newCar.transform.rotation = o.transform.rotation;
            }
        }
        for (int i = 1; i <= 6; i++) {
            dumpsterSpawns.Add(dumpsterObj.transform.Find("Spawn" + i).gameObject);
        }
        foreach(GameObject o in dumpsterSpawns) {
            if (Random.Range(0.0f, 1f) < .1f) {
                GameObject newDumpster = Instantiate(dumpster);
                newDumpster.transform.parent = o.transform;
                newDumpster.transform.position = o.transform.position + new Vector3(0f, 1f, 0f);
                newDumpster.transform.position += new Vector3(0f, 0f, Random.Range(-PlotSize.y / 3f, PlotSize.y / 3f));
            }
        }
        for (int i = 1; i <= 4; i++) {
            garbageSpawns.Add(garbageObj.transform.Find("Spawn" + i).gameObject);
        }
        foreach(GameObject o in garbageSpawns) {
            if (Random.Range(0.0f, 1f) < .4f) {
                GameObject newGarbage = Instantiate(trashCan);
                newGarbage.transform.parent = o.transform;
                newGarbage.transform.position = o.transform.position + new Vector3(0f, 1f, 0f);
            }
        }
        for (int i = 1; i <= 8; i++)
        {
            plots.Add(plotObj.transform.Find("Plot" + i).gameObject);
        }
        if(Random.Range(0f,100f) > emptyPlotPercentage) {
            foreach (GameObject o in plots) {
                GameObject newBuilding = Instantiate(buildings[Random.Range(0, buildings.Count)]);
                newBuilding.transform.parent = o.transform;
                float movableX = (PlotSize.x - newBuilding.GetComponent<Building>().size.x) / 2f;
                float movableY = (PlotSize.y - newBuilding.GetComponent<Building>().size.z) / 2f;
                Vector3 move = new Vector3(Random.Range(-movableX, movableX), 0f, Random.Range(-movableY, movableY));
                newBuilding.transform.position = o.transform.position + move;
            }
        }
        SpawnCops(newChunk);
    }
    public void SpawnCops(GameObject newChunk) {
        //fill in list of waypoints
        List<GameObject> waypoints = new List<GameObject>();
        foreach (Transform c in newChunk.transform.GetChild(1).GetChild(0)) {
            waypoints.Add(c.gameObject);
        }
        for (int i = 0; i < waypoints.Count; i++) {
            if (Random.Range(0f, 100f) < spawnPercentage && Vector3.Distance(player.transform.position, waypoints[i].transform.position) >= 15f) {
                GameObject newCop = Instantiate(cop, waypoints[i].transform);
                newCop.transform.parent = waypoints[i].transform;
                newCop.transform.position = new Vector3(waypoints[i].transform.position.x, .1f, waypoints[i].transform.position.z);
                //setup cop script
                Cop c = newCop.GetComponent<Cop>();
                c.SetSpawnPoint(waypoints[i]);
                c.CreatePatrolPath();
                //Debug.LogWarning("Spawned cop!");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        difficultyTimer -= Time.deltaTime;
        if(difficultyTimer <= 0f) {
            difficultyTimer = difficultyIncreaseTime;
            spawnPercentage += .5f;
            Cop prefab = cop.GetComponent<Cop>();
            prefab.patrolSpeed += .15f;
            prefab.pursuitSpeed += .15f;
            prefab.maxRecognitionTime -= .05f;
            Debug.Log(cop.GetComponent<Cop>().pursuitSpeed);
        }
        //update chunk location
        playerX = (int)((player.transform.position.x + ((renderDist + .5f) * blockSize.x)) / blockSize.x);
        playerY = (int)((player.transform.position.z + ((renderDist + .5f) * blockSize.y)) / blockSize.y);
        //Debug.Log("X:" + playerX + ", Y:" + playerY);
        
        //compare old variables to new ones to see if character has jumped chunks
        if(playerX > oldX)
        {
            List<GameObject> oldObjs = levelMap[0];
            levelMap.RemoveAt(0);
            foreach(GameObject e in oldObjs)
            {
                Destroy(e);
            }
            levelMap.Add(new List<GameObject>());
            for (int y = 0; y < renderDist * 2; y++)
            {
                GameObject newChunk = Instantiate<GameObject>(block, level);
                
                levelMap[levelMap.Count - 1].Add(newChunk);
                //position it around player
                newChunk.transform.position = new Vector3((renderDist * blockSize.x * -1) + ((playerX + renderDist - 1) * blockSize.x), 0f, (renderDist * blockSize.y * -1) + ((playerY - renderDist + y) * blockSize.y));
                FillPlots(newChunk);
                
            }
        }
        else if(playerX < oldX)
        {
            List<GameObject> oldObjs = levelMap[levelMap.Count - 1];
            levelMap.RemoveAt(levelMap.Count-1);
            foreach (GameObject e in oldObjs)
            {
                Destroy(e);
            }
            levelMap.Insert(0,new List<GameObject>());
            for (int y = 0; y < renderDist * 2; y++)
            {
                GameObject newChunk = Instantiate<GameObject>(block, level);
                
                levelMap[0].Add(newChunk);
                //position it around player
                newChunk.transform.position = new Vector3((renderDist * blockSize.x * -1) + ((playerX - renderDist) * blockSize.x), 0f, (renderDist * blockSize.y * -1) + ((playerY - renderDist + y) * blockSize.y));
                FillPlots(newChunk);
                //newChunk.GetComponent<NavMeshSurface>().BuildNavMesh();
                
            }
        }

        if(playerY > oldY)
        {
            List<GameObject> oldObjs = new List<GameObject>();

            //add the entire row to list
            for (int i = 0; i < levelMap.Count; i++)
            {
                oldObjs.Add(levelMap[i][0]);
                levelMap[i].RemoveAt(0);
            }
            foreach (GameObject e in oldObjs)
            {
                Destroy(e);
            }

            for (int y = 0; y < renderDist * 2; y++)
            {
                GameObject newChunk = Instantiate<GameObject>(block, level);
                
                levelMap[y].Insert(levelMap.Count - 1, newChunk);
                //levelMap[0].Add(newChunk);
                //position it around player
                newChunk.transform.position = new Vector3((renderDist * blockSize.x * -1) + ((playerX - renderDist + y) * blockSize.x), 0f, (renderDist * blockSize.y * -1) + ((playerY + renderDist - 1) * blockSize.y));
                FillPlots(newChunk);
                //newChunk.GetComponent<NavMeshSurface>().BuildNavMesh();
            }
        }
        else if(playerY < oldY)
        {
            List<GameObject> oldObjs = new List<GameObject>();
            
            //add the entire row to list
            for (int i = 0; i < levelMap.Count; i++)
            {
                oldObjs.Add(levelMap[i][levelMap.Count - 1]);
                levelMap[i].Remove(oldObjs[oldObjs.Count-1]);
            }
            foreach (GameObject e in oldObjs)
            {
                Destroy(e);
            }
            
            for (int y = 0; y < renderDist * 2; y++)
            {
                GameObject newChunk = Instantiate<GameObject>(block, level);
                
                levelMap[y].Insert(0, newChunk);
                //levelMap[0].Add(newChunk);
                //position it around player
                newChunk.transform.position = new Vector3((renderDist * blockSize.x * -1) + ((playerX - renderDist + y) * blockSize.x), 0f, (renderDist * blockSize.y * -1) + ((playerY - renderDist) * blockSize.y));
                FillPlots(newChunk);
                //newChunk.GetComponent<NavMeshSurface>().BuildNavMesh();
            }
        }

        //update old variables
        oldX = playerX;
        oldY = playerY;
        
    }
    
}
