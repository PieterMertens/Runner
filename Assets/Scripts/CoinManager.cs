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
    int streakLength;

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

        if ((nextPositionToHandle+70)%250 >= 250-(16*spacing)) {
            int difference = 250 - ((nextPositionToHandle+70) % 250);
            nextPositionToHandle += (40+difference);
        }

        if ((nextPositionToHandle+70)%250 <= 40) {
            nextPositionToHandle += (40 - ((nextPositionToHandle+70) % 250));
        }

        if (currentZCo == nextPositionToHandle){
            streakLength = Random.Range(10, 16);
            nextPositionToHandle += streakLength*spacing*2;
            randomSpawnCoin(streakLength, currentZCo);
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

    private void randomSpawnCoin(int streakLength, int currentZCo) {
        //// REIMPLEMENTATION

        float lastOpeningZ = manager.getLastOpening(currentZCo + 70);
        float nextOpeningZ = manager.getNextOpening(000022 +70);
        int nextOpeningValue = manager.getOpeningAt(nextOpeningZ);

        int lastLane = manager.getOpeningAt(lastOpeningZ);
        int lastChanged = 0;

        for (int i = 0; i < streakLength; i++) {
            float coinX = 0;
            float coinY = 1.5f;
            float coinZ = currentZCo + 70 + i * spacing;

            //// Y-value
            if ((nextOpeningZ - coinZ == 0 || nextOpeningZ - coinZ == 1) && !manager.noJump(nextOpeningZ)) {
                coinY = 1.75f;
            }

            //// Z-value
            if (coinZ - nextOpeningZ > 1) {
                lastOpeningZ = nextOpeningZ;
                nextOpeningZ = manager.getNextOpening(coinZ);
                nextOpeningValue = manager.getOpeningAt(nextOpeningZ);
            }

            //// X-value
            if (shouldChange(coinZ,nextOpeningZ,nextOpeningValue, lastLane,i , lastChanged)) {
                coinX = nextOpeningValue;
            } else {
                if (canChange(coinZ, nextOpeningZ, lastOpeningZ, i, lastChanged)) {
                    coinX = getRandomLane(lastLane, nextOpeningValue);
                }
                else {
                    coinX = lastLane;
                }
            }

            spawnCoin(new Vector3(coinX, coinY, coinZ));
            if (lastLane != Mathf.RoundToInt(coinX)){
                lastChanged = i;
                lastLane = Mathf.RoundToInt(coinX);
            }
        }
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

    private bool shouldChange(float coinZ, float nextOpeningZ, int nextOpeningValue, int currentLane, int current, int lastChanged) {
        return ((nextOpeningZ-coinZ <= 3*spacing) && nextOpeningValue != currentLane && ((current-lastChanged >= 3) || current <=2));
    }

    private bool canChange(float coinZ, float nextOpeningZ, float lastOpeningZ, int current, int lastChanged) {
        return ((nextOpeningZ - coinZ > 4*spacing) && (current-lastChanged >= 3) && (coinZ-lastOpeningZ > 3* spacing));
    }

    private int getRandomLane(int lastLane, int nextOpeningValue) {
        return Random.Range(Mathf.Max(-1, lastLane - 1, nextOpeningValue -1), Mathf.Min(2, lastLane + 2, nextOpeningValue + 2));
    }
}
