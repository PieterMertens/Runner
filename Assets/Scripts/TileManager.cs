using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePrefabs;

  
    private Transform playerTransform;

    public float tileLength = 10f;
    private float spawnZ = 0f;
    public int amountOfTiles =8;

    private List<GameObject> activeTiles;
    private float safeZone = 11f;

    private int lastPrefabIndex = 0;


    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activeTiles = new List<GameObject>();

        //TODO begin speciaal maken of duidelijk
        for (int i = 0; i < amountOfTiles; i++)
        {
            if (i < 3) { SpawnTile(0); }
            else
            {
                SpawnTile();
            }
        }
       
        
	}
	
	// Update is called once per frame
	void Update () {

        if (playerTransform.position.z - safeZone > (spawnZ - amountOfTiles*tileLength)) {
            
            SpawnTile();
            DeleteTile();
        }
		
	}

    private void SpawnTile(int prefabIndex = -1) {
        GameObject go;
        if (prefabIndex == 0)
        {
            go = Instantiate(tilePrefabs[0]) as GameObject;
        }
                else
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);


    }

    private int RandomPrefabIndex() {
        if (tilePrefabs.Length <= 1){
            return 0;
        }

        int randomIndex = Random.Range(1,tilePrefabs.Length);
        while (randomIndex == lastPrefabIndex) {
            randomIndex = Random.Range(1, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;

    }


    private void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);       
    }


}
