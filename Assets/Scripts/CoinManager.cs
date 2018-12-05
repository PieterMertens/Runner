using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    // TEST //
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

    // Use this for initialization
    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        coins = new List<GameObject>();

        nextPositionToHandle = 0;
    }

    // Update is called once per frame
    void Update() {
        foreach (GameObject coin in coins.ToArray()) {
            if (checkIfPassed(coin)) {
                deleteCoin(coin);
            }
        }

        int currentZCo = Mathf.RoundToInt(playerTransform.position.z);

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
            if (i - positionChanged < 3) {
                spawnCoin(new Vector3(lastXCo, 1, currentZCo + 40 + i * spacing));
            }

            else{
                int xCo = Random.Range(randomLanes - 1, randomLanes + 1);
                spawnCoin(new Vector3(xCo, 1, currentZCo + 40 + i * spacing));
                if (lastXCo != xCo) {
                    lastXCo = xCo;
                    positionChanged = i;
                }
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
}
