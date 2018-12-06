using UnityEngine;
using System.Collections.Generic;
using System;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePrefabs;

    public GameObject[] obstaclesHigh;

    public GameObject[] obstaclesLow;

  
    private Transform playerTransform;

    public float tileLength = 10f;
    private float spawnZ = 0f;
    public int amountOfTiles =8;

    private List<GameObject> activeTiles;
    private List<GameObject> activeObstacles;
    private float safeZone = 11f;

    private int lastPrefabIndex = 0;

    private int previousOpening = 0;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activeTiles = new List<GameObject>();
        activeObstacles = new List<GameObject>();

        //TODO begin speciaal maken of duidelijk
        for (int i = 0; i < amountOfTiles; i++)
        {
            if (i < 3) {
                SpawnTile(0);
            }
            else
            {
                int colorTile = UnityEngine.Random.Range(0, 4);
                int colorObstacle = UnityEngine.Random.Range(0, 3);
                while (colorTile == lastPrefabIndex) {
                    colorTile = UnityEngine.Random.Range(0, 4);
                }
                lastPrefabIndex = colorTile;
                if (colorObstacle >= colorTile) {
                    colorObstacle += 1;
                }

                SpawnObstacles(colorObstacle);
                SpawnTile(colorTile);
            }
        }
       
        
	}
	
	// Update is called once per frame
	void Update () {
        float position = playerTransform.position.z;

        if (position - safeZone > (spawnZ - amountOfTiles*tileLength)) {

            int colorTile = UnityEngine.Random.Range(0, 4);
            int colorObstacle = UnityEngine.Random.Range(0, 3);
            while (colorTile == lastPrefabIndex) {
                colorTile = UnityEngine.Random.Range(0, 4);
            }
            lastPrefabIndex = colorTile;
            if (colorObstacle >= colorTile) {
                colorObstacle += 1;
            }

            SpawnObstacles(colorObstacle);
            DeleteObstacles(position);

            SpawnTile(colorTile);
            DeleteTile();
        }
		
	}

    private void SpawnTile(int colorTile) {
        GameObject go = Instantiate(tilePrefabs[colorTile]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);


    }

    private void SpawnObstacles(int colorObstacle) {
        int opening = getNewOpening(previousOpening);

        bool spreaded = (UnityEngine.Random.Range(0, 2) == 0);
        bool front = false;
        if (spreaded) { front = UnityEngine.Random.Range(0, 2) == 0; }
        bool jump = (UnityEngine.Random.Range(0, 10)%5 != 0);
        List<int> otherObstacles = getOtherObstacles();

        float posZ = spawnZ + UnityEngine.Random.Range(-1, 2);

        if (!spreaded || front && jump) {
                SpawnObstacle(colorObstacle, new Vector3(opening, 0.75f, posZ), false);
            }
        if (spreaded && !front && jump) {
                SpawnObstacle(colorObstacle, new Vector3(opening, 0.75f, posZ+1), false);
            }
        if (!spreaded || !front) {
                for (int j = -1; j < 2; j++) {
                    if (j == opening) {
                        continue;
                    }
                    else {
                        int y = otherObstacles[0];
                        if (y != 0) {
                            SpawnObstacle(colorObstacle, new Vector3(j, 0.75f +(y-1)*0.75f, posZ), y == 2);
                        }
                        otherObstacles.RemoveAt(0);
                    }
                }
            }

        if (spreaded && front) {
                for (int j = -1; j < 2; j++) {
                    if (j == opening) {
                        continue;
                    }
                    else {
                        int y = otherObstacles[0];
                        if (y != 0) {
                            SpawnObstacle(colorObstacle, new Vector3(j, 0.75f+(y-1)*0.75f, posZ+1), y == 2);
                        }
                        otherObstacles.RemoveAt(0);
                    }
                }
            }

        previousOpening = opening;
    }

    private int getNewOpening(int previousOpening) {
        int newOpening;
        if (previousOpening == 0) {
            newOpening = UnityEngine.Random.Range(-1, 2);
        }
        else {
            newOpening = UnityEngine.Random.Range(0, 2) * previousOpening;
        }
        return newOpening;
    }

    private List<int> getOtherObstacles() {
        List<int> result = new List<int>();
        for (int i = 0; i <2; i++)  {
            bool high = (UnityEngine.Random.Range(0, 10) % 5 != 0);
            bool low = (UnityEngine.Random.Range(0, 10) % 3 != 0);
            if (high) { result.Add(2); }
            if (!high && low) { result.Add(1); }
            if (!high && !low) { result.Add(0); }
        }
        return result;
    }

    private void SpawnObstacle(int colorObstacle, Vector3 position, bool high) {
        GameObject obstacle;
        if (high) {
            obstacle = Instantiate(obstaclesHigh[colorObstacle]) as GameObject;
        }
        else {
            obstacle = Instantiate(obstaclesLow[colorObstacle]) as GameObject;
        }
        obstacle.transform.SetParent(transform);
        obstacle.transform.position = position;
        activeObstacles.Add(obstacle);
    }

    private void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);       
    }

    private void DeleteObstacles(float position) {
        float limit = position - safeZone;
        bool limitReached = false;
        while (limitReached == false) {
            GameObject obstacle = activeObstacles[0];
            if (obstacle.transform.position.z < limit) {
                activeObstacles.Remove(obstacle);
                Destroy(obstacle);
            }
            else {
                limitReached = true;
            }
        }
    }
}
