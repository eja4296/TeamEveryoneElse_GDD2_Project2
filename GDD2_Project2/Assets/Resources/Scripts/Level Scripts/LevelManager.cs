using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    //stores player location
    public Transform player;
    //stores level transform to parent new level elements
    public Transform level;

    List<List<GameObject>> levelMap;

    public GameObject block;

    //Y axis in vector2 is measurement of Z in game
    public Vector2 blockSize;

    //the number of chunks the level should be generated to in the X and the Y
    public int renderDist;
    int oldX, oldY;
    public int playerX, playerY;
    // Use this for initialization
    void Start () {
        //calculate half sizes
        block = Resources.Load<GameObject>("Prefabs/Level Prefabs/block");
        levelMap = new List<List<GameObject>>();
        while (levelMap.Count < renderDist * 2)
        {
            levelMap.Add(new List<GameObject>());
        }
        for (int x = 0; x < levelMap.Count; x++)
        {
            for (int y = 0; y < renderDist * 2; y++)
            {
                GameObject newChunk = Instantiate<GameObject>(block, level);
                levelMap[x].Add(newChunk);
                //position it around player
                newChunk.transform.position = new Vector3((renderDist * blockSize.x *  -1) + (x * blockSize.x), 0f, (renderDist * blockSize.y * -1) + (y * blockSize.y));
            }
        }
        playerX = (int)((player.transform.position.x + ((renderDist + .5f) * blockSize.x)) / blockSize.x);
        playerY = (int)((player.transform.position.z + ((renderDist + .5f) * blockSize.y)) / blockSize.y);
        oldX = playerX;
        oldY = playerY;
   
    }

    // Update is called once per frame
    void Update()
    {
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
            }
        }

        //update old variables
        oldX = playerX;
        oldY = playerY;

    }
    
}
