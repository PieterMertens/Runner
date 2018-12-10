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

    public List<GameObject> activeTiles;
    public List<GameObject> activeObstacles;
    private float safeZone = 11f;

    private int lastPrefabIndex = 0;

    private int previousOpening = 0;

    private int nextObstacleSpwan = 40;
    private int obstaclesSpacing = 11;

    private Dictionary<float, int> openings;
    private Dictionary<float, int> tilesColor;
    private List<float> noJumps;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activeTiles = new List<GameObject>();
        activeObstacles = new List<GameObject>();
        openings = new Dictionary<float, int>();
        noJumps = new List<float>();
        tilesColor = new Dictionary<float, int>();

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

                SpawnTile(colorTile);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        float position = playerTransform.position.z;

        if (position - safeZone > (spawnZ - amountOfTiles*tileLength)) {

            int colorTile = UnityEngine.Random.Range(0, 4);

            while (colorTile == lastPrefabIndex) {
                colorTile = UnityEngine.Random.Range(0, 4);
            }
            lastPrefabIndex = colorTile;

            SpawnTile(colorTile);
            DeleteTile();
        }

        if (nextObstacleSpwan - Mathf.RoundToInt(position) <= 120) {
            if (!(nextObstacleSpwan % 250 < 40) && !(nextObstacleSpwan % 250 >= 240))
            {
                SpawnObstacles(nextObstacleSpwan);
                DeleteObstacles(position);
            }
            nextObstacleSpwan += obstaclesSpacing;
        }		
	}

    private void SpawnTile(int colorTile) {
        GameObject go = Instantiate(tilePrefabs[colorTile]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        tilesColor.Add(spawnZ, colorTile);
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void DeleteTile() {
        tilesColor.Remove(activeTiles[0].transform.position.z);
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private void SpawnObstacles(int zCo) {
        int opening = getNewOpening(previousOpening);

        bool spreaded = (UnityEngine.Random.Range(0, 2) == 0);
        bool front = false;
        if (spreaded) { front = UnityEngine.Random.Range(0, 2) == 0; }
        bool jump = (UnityEngine.Random.Range(0, 10)%5 != 0);
        List<int> otherObstacles = getOtherObstacles();

        float posZ = zCo + UnityEngine.Random.Range(-1, 2);

        int newColor = getNewColor(posZ);

        if (!jump) {
            openings.Add(posZ,opening);
            noJumps.Add(posZ);
        }
        if ((!spreaded || front) && jump) {
            SpawnObstacle(newColor, new Vector3(opening, 0.75f, posZ), false);
            openings.Add(posZ, opening);
        }
        if (spreaded && !front && jump) {
            SpawnObstacle(newColor, new Vector3(opening, 0.75f, posZ+1), false);
            openings.Add(posZ + 1, opening);
        }
        if (!spreaded || !front) {
                for (int j = -1; j < 2; j++) {
                    if (j == opening) { continue;}
                    else {
                        int y = otherObstacles[0];
                        if (y != 0) {
                            SpawnObstacle(newColor, new Vector3(j, 0.75f +(y-1)*0.75f, posZ), y == 2);
                        }
                        otherObstacles.RemoveAt(0);
                    }
                }
            }

        if (spreaded && front) {
                for (int j = -1; j < 2; j++) {
                    if (j == opening) { continue; }
                    else {
                        int y = otherObstacles[0];
                        if (y != 0) {
                            SpawnObstacle(newColor, new Vector3(j, 0.75f+(y-1)*0.75f, posZ+1), y == 2);
                        }
                        otherObstacles.RemoveAt(0);
                    }
                }
            }

        previousOpening = opening;
    }

    private void SpawnObstacle(int colorObstacle, Vector3 position, bool high) {
        GameObject obstacle;
        if (high)
        {
            obstacle = Instantiate(obstaclesHigh[colorObstacle]) as GameObject;
        }
        else
        {
            obstacle = Instantiate(obstaclesLow[colorObstacle]) as GameObject;
        }
        obstacle.transform.SetParent(transform);
        obstacle.transform.position = position;
        activeObstacles.Add(obstacle);
    }

    private void DeleteObstacles(float position) {
        float limit = position - safeZone;
        bool limitReached = false;
        while (limitReached == false)
        {
            GameObject obstacle = activeObstacles[0];
            if (obstacle.transform.position.z < limit)
            {
                if (openings.ContainsKey(obstacle.transform.position.z))
                {
                    openings.Remove(obstacle.transform.position.z);
                }
                if (noJumps.Contains(obstacle.transform.position.z))
                {
                    noJumps.Remove(obstacle.transform.position.z);
                }
                activeObstacles.Remove(obstacle);
                Destroy(obstacle);
            }
            else
            {
                limitReached = true;
            }
        }
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

    private List<int> getColor(float z) {
        List<int> colors = new List<int>();
        foreach (KeyValuePair<float, int> tileColor in tilesColor)
        {
            if (Mathf.Abs(tileColor.Key - z) <= 5)
            {
                colors.Add(tileColor.Value);
            }
        }
        return colors;
    }

    private int getNewColor(float z) {
        List<int> tileColorFront = getColor(z);
        List<int> tileColorBack = getColor(z + 1);

        List<int> oldColors = tileColorFront;
        foreach (int oldColor in tileColorBack)
        {
            if (!oldColors.Contains(oldColor)) { oldColors.Add(oldColor); }
        }

        int newColor = UnityEngine.Random.Range(0, 4);
        while (oldColors.Contains(newColor))
        {
            newColor = UnityEngine.Random.Range(0, 4);
        }

        return newColor;
    }

    public void updateObstaclesSpacing() {
        obstaclesSpacing += 1;
    }

    public float getNextOpening(float z) {
        foreach (KeyValuePair<float, int> opening in openings) {
            if (opening.Key - z >=0 && opening.Key - z <= obstaclesSpacing) {
                return opening.Key;
            }
        }
        return -1;
    }

    public float getLastOpening(float z) {
        foreach (KeyValuePair<float, int> opening in openings) {
            if (z - opening.Key >= 0 && z - opening.Key <= obstaclesSpacing) {
                return opening.Key;
            }
        }
        return -1;
    }

    public int getOpeningAt(float z) {
        if (z == -1) { return 0; ; }
        else { return openings[z]; }
    }

    public bool noJump(float z) {
        return noJumps.Contains(z);
    }
}
