using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    //Prefab coin used//
    public GameObject coinPrefab;

    //List storing all coins in the game at a given time//
    private List<GameObject> coins;

    //Information about position player//
    private Transform playerTransform;

    //Integer reflecting a random length for a coin streak, updated after every streak of coins//
    int randomSpawn;

    //Spacing between coins
    int spacing = 2;

    //The next position that something needs to happen//
    int nextPositionToHandle;

    GameObject tileManager;
    TileManager manager;
    List<GameObject> obstacles;

    // Use this for initialization
    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        coins = new List<GameObject>();
        tileManager = GameObject.Find("TileManager");
        manager = tileManager.GetComponent<TileManager>();

        nextPositionToHandle = 0;

        obstacles = manager.activeObstacles;
    }

    // Update is called once per frame
    void Update() {
        obstacles = manager.activeObstacles;
        foreach (GameObject coin in coins.ToArray()) {
            if (checkIfPassed(coin)) {
                deleteCoin(coin);
            }
        }

        int currentZCo = Mathf.RoundToInt(playerTransform.position.z);

        if (nextPositionToHandle%250 >= 220) {
            int difference = 250 - (nextPositionToHandle % 250);
            nextPositionToHandle += (40+difference);
        }

        if (nextPositionToHandle%250 <= 40) {
            nextPositionToHandle += (40 - (nextPositionToHandle % 250));
        }

        if (currentZCo == nextPositionToHandle){
            randomSpawn = Random.Range(10, 16);
            nextPositionToHandle += randomSpawn*spacing*2;
            randomSpawnCoin(randomSpawn, currentZCo);
        }
    }

    private bool checkIfPassed(GameObject coin) {
        float zCoordinate = coin.transform.position.z;
        if (zCoordinate < playerTransform.position.z - 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void randomSpawnCoin(int randomSpawn, int currentZCo) {
        int randomLanes = Random.Range(0, 2);
        int positionChanged = 0;
        int startLane = Random.Range(randomLanes - 1, randomLanes + 1);
        int lastXCo = startLane;
        for (int i = 0; i < randomSpawn; i++){
            int otherXCo;
            if (lastXCo == randomLanes) {
                otherXCo = lastXCo - 1;
            }
            else {
                otherXCo = lastXCo + 1;
            }
            if (i - positionChanged < 3 || !canChangeLane(otherXCo, currentZCo+40+i-spacing)) {
                if (canSpawnCoin(lastXCo, currentZCo + 40 + i * spacing, false)) {
                    spawnCoin(new Vector3(lastXCo, 1, currentZCo + 40 + i * spacing));
                } else {
                    randomSpawn += 1;
                }
            }

            else{
                int xCo = Random.Range(randomLanes - 1, randomLanes + 1);
                if (canSpawnCoin(xCo, currentZCo + 40 + i * spacing, false)) {
                    spawnCoin(new Vector3(xCo, 1, currentZCo + 40 + i * spacing));
                }
                else {
                    randomSpawn += 1;
                }
                if (lastXCo != xCo) {
                    lastXCo = xCo;
                    positionChanged = i;
                }
            }
        }
    }

    private bool canSpawnCoin(int xCo, int zCo, bool checkForHighOnly) {
        foreach (GameObject obstacle in obstacles) {
            Vector3 positionObstacle = obstacle.transform.position;
            float zCoObstacle = positionObstacle.z;
            float xCoObstacle = positionObstacle.x;
            if (xCo == xCoObstacle && Mathf.Abs(zCo - zCoObstacle) <= 1) {
                if (!checkForHighOnly) {
                    return false;
                }
                else {
                    if (obstacle.transform.lossyScale.y == 2) {
                        return false;
                    }
                }
                return false;
            }
        }
        return true;
    }

    private bool canChangeLane(int pos, int xCo) {
        for (int i = 0; i < 3; i++) {
            if (!canSpawnCoin(xCo, pos + i, true)) {
                return false;
            }
        }
        return true;
    }

    private void spawnCoin(Vector3 position) {
        GameObject spawnedCoin = Instantiate(coinPrefab, position, coinPrefab.transform.rotation);
        spawnedCoin.transform.SetParent(transform);
        coins.Add(spawnedCoin);
    }

    public void deleteCoin(GameObject coin) {
        int index = coins.IndexOf(coin);
        coins.RemoveAt(index);
        Destroy(coin);
    }
}
