using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    //stores player location
    public Transform player;
    //stores level transform to parent new level elements
    public Transform level;

    public GameObject block;
    public GameObject street;
    //horizontally oriented street on the short sides of blocks
    public GameObject horizStreet;
    public GameObject intersection;

    //Y axis in vector2 is measurement of Z in game
    public Vector2 blockSize;
    public Vector2 streetSize;
    public Vector2 intersectionSize;
    public Vector2 horizStreetSize;

    Vector2 blockHalfSize;
    Vector2 streetHalfSize;
    Vector2 intersectionHalfSize;
    Vector2 horizStreetHalfSize;

    //the number of chunks the level should be generated to in the X and the Y
    public int renderDist;


    // Use this for initialization
    void Start () {
        //calculate half sizes
        blockHalfSize = blockSize / 2f;
        streetHalfSize = streetSize / 2f;
        intersectionHalfSize = intersectionSize / 2f;
        horizStreetHalfSize = horizStreetSize / 2f;

        block = Resources.Load<GameObject>("Prefabs/Level Prefabs/Block1");//using block1 is temp
        street = Resources.Load<GameObject>("Prefabs/Level Prefabs/Street");
        intersection = Resources.Load<GameObject>("Prefabs/Level Prefabs/Intersection");
        horizStreet = Resources.Load<GameObject>("Prefabs/Level Prefabs/Horizontal Street");

        Vector2 spawnPos = -intersectionSize/2f;
        //for creating block pattern
        int xIndex = 0;
        int yIndex = 0;
        //loop the rows of patterns
        for (int z = 0; z < renderDist/4; z++) {
            //Create rows of patterned chunks
            for (int x = 0; x < renderDist; x++) {
                //align the spawn point to the half widths to get aligned spawning
                if (xIndex == 0) {
                    spawnPos.x += streetHalfSize.x;
                } else if (xIndex == 1) {
                    spawnPos.x += blockHalfSize.x;
                }

                float oldSpawnY = spawnPos.y;
                //create columns of patterned chunks

                for (int y = 0; y < renderDist; y++) {
                    //for the first type of column pattern (streets)
                    if (xIndex == 0) {
                        switch (yIndex) {
                            case 0:
                                spawnPos.y += intersectionHalfSize.y;
                                GameObject newIntersection = Instantiate(intersection, level);
                                newIntersection.transform.position = new Vector3(spawnPos.x, 0f, spawnPos.y);
                                spawnPos.y += intersectionHalfSize.y;
                                break;
                            case 1:
                                spawnPos.y += streetHalfSize.y;
                                GameObject newStreet = Instantiate(street, level);
                                newStreet.transform.position = new Vector3(spawnPos.x, 0f, spawnPos.y);
                                spawnPos.y += streetHalfSize.y;
                                break;
                            case 2:
                                spawnPos.y += streetHalfSize.y;
                                newStreet = Instantiate(street, level);
                                newStreet.transform.position = new Vector3(spawnPos.x, 0f, spawnPos.y);
                                spawnPos.y += streetHalfSize.y;
                                break;
                        }
                    } 
                    //for the second type of column pattern (blocks)
                    else if (xIndex == 1) {

                        switch (yIndex) {
                            case 0:
                                spawnPos.y += horizStreetHalfSize.y;
                                GameObject newHorizStreet = Instantiate(horizStreet, level);
                                newHorizStreet.transform.position = new Vector3(spawnPos.x, 0f, spawnPos.y);
                                spawnPos.y += horizStreetHalfSize.y;
                                break;
                            case 1:
                                spawnPos.y += blockHalfSize.y;
                                GameObject newBlock = Instantiate(block, level);
                                newBlock.transform.position = new Vector3(spawnPos.x, 0f, spawnPos.y);
                                spawnPos.y += blockHalfSize.y;
                                break;
                            case 2:
                                spawnPos.y += blockHalfSize.y;
                                newBlock = Instantiate(block, level);
                                newBlock.transform.position = new Vector3(spawnPos.x, 0f, spawnPos.y);
                                spawnPos.y += blockHalfSize.y;
                                break;
                        }
                    }
                    //increment y
                    yIndex++;
                }
                //reset the Y for the next column
                spawnPos.y = oldSpawnY;
                //add second half size
                if (xIndex == 0) {
                    spawnPos.x += streetHalfSize.x;
                } else if (xIndex == 1) {
                    spawnPos.x += blockHalfSize.x;
                }
                //increment x
                xIndex++;
                //reset if over pattern limit
                if (xIndex > 1) xIndex = 0;
                if (yIndex > 2) yIndex = 0;
            }
            //reset positions for next row
            spawnPos.y += streetSize.y * 2f + intersectionSize.y;
            spawnPos.x = -intersectionSize.x / 2f;
            //reset indices to start on the same section of the pattern
            xIndex = 0;
            yIndex = 0;
        }
    }

    // Update is called once per frame
    void Update() {
        
	}
    
}
