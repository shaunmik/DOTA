using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {
    public GameObject ghostWhite;
    public GameObject ghostViolet;

    [SerializeField]
    private List<GameObject> monsters;

    private float spawnTime = 1f;
    private float maxTime = 10f;
    private float minTime = 5f;
    private float time = 0;
    private float difficultyIncreaseTime=60f;
    private float currentDifficultyIncreaseTime = 30f;
    private int amountOfEnemies = 2;

    // Use this for initialization
    void Start () {
        //Sets when the monster should first spawn
        spawnTime = Random.Range(minTime, maxTime);
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        currentDifficultyIncreaseTime += Time.deltaTime;
        
        if (currentDifficultyIncreaseTime>= difficultyIncreaseTime)
        {
            currentDifficultyIncreaseTime = 0f;
            minTime = minTime / 2;
            maxTime = maxTime / 2;
        }

        //When it's time to spawn another monster, does so and resets the next time to spawn
        if (time >= spawnTime)
        {
            //Resets timer
            time = 0;
            spawnTime = Random.Range(minTime, maxTime);

            //Randomly picks monster to spawn and spawns it
            int chosenMonster = Random.Range(0, amountOfEnemies);
            if (chosenMonster == 0)
            {
                SpawnGhostWhite();
            }
            //if (chosenMonster == 1)
            if (chosenMonster == 1)
            {
                SpawnGhostViolet();
            }
        }
    }

    void GetSpawnPoint(out Vector3 spawnPos) {
        //Randomizes position of monster
        int spawnPositionX = Random.Range(16, 300);
        spawnPos = new Vector3(spawnPositionX, 5f, 379);
        spawnPos.y = Terrain.activeTerrain.SampleHeight(spawnPos);
    }

    void SpawnMonster(GameObject monster, Vector3 spawnPos) {
        GameObject newMonster = GameObject.Instantiate(monster, spawnPos, Quaternion.identity);
        newMonster.SetActive(true);
    }

    void SpawnGhostWhite()
    {
        // Get monster spawn point
        Vector3 position;
        GetSpawnPoint(out position);

        //Creates monster
        SpawnMonster(ghostWhite, position);
    }

    void SpawnGhostViolet()
    {
        // Get monster spawn point
        Vector3 position;
        GetSpawnPoint(out position);

        //Creates monster
        SpawnMonster(ghostViolet, position);
    }
}
