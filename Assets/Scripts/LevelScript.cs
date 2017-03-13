using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelScript : MonoBehaviour {
    public GameObject[] monsters;

    /*
    public GameObject ghostWhite;
    public GameObject ghostViolet;
    public GameObject rabbitYellow;
    */
    private float spawnTime = 1f;
    private float maxTime = 10f;
    private float minTime = 5f;
    private float time = 0;
    
    private float difficultyIncreaseTime=60f;
    private float currentDifficultyIncreaseTime = 30f;
    private int amountOfEnemies;

    private Dictionary<GameObject, Vector3> MonsterToSpawnOffsetMap;

    void Start () {
        //Sets when the monster should first spawn
        spawnTime = 0; //TODO: Random.Range(minTime, maxTime);
        Cursor.visible = false;
        amountOfEnemies = monsters.Length;
        MonsterToSpawnOffsetMap = Monsters.createMonsterToSpawnOffsetMap();
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        currentDifficultyIncreaseTime += Time.deltaTime;
        
        if (currentDifficultyIncreaseTime>= difficultyIncreaseTime)
        {
            currentDifficultyIncreaseTime = 0f;
            minTime = minTime / 1.3f;
            maxTime = maxTime / 1.3f;
        }

        //When it's time to spawn another monster, does so and resets the next time to spawn
        if (time >= spawnTime)
        {
            //Resets timer
            time = 0;
            spawnTime = Random.Range(minTime, maxTime);

            //Randomly picks monster to spawn and spawns it
            int chosenMonster = Random.Range(0, amountOfEnemies);
            
            SpawnMonster(monsters[chosenMonster]);
        }
    }

    void GetSpawnPoint(out Vector3 spawnPos, GameObject monster) {
        // Randomizes position of monster
        // TODO: replace this function with actual spawn points or range as param
        int spawnPositionX = Random.Range(-70, 70);
        spawnPos = new Vector3(spawnPositionX, 5f, 379f);
        // REMOVED: spawnPos.y = Terrain.activeTerrain.SampleHeight(spawnPos);

        NavMeshHit navHit;
 
        NavMesh.SamplePosition (spawnPos, out navHit, 15.0f, 1 << NavMesh.GetAreaFromName("Walkable"));

        spawnPos = navHit.position + MonsterToSpawnOffsetMap[monster];
    }

    void SpawnMonster(GameObject monster) {
        // Get monster spawn point
        Vector3 position;
        GetSpawnPoint(out position, monster);

        // Instantiate monster
        GameObject newMonster = GameObject.Instantiate(monster, position, Quaternion.identity);
        newMonster.SetActive(true);
    }

}
