using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1Script : MonoBehaviour {
    [Header("BGM")]
    public AudioSource[] BGM;

    [Header("Level 1")]
    public GameObject[] monstersLevel1;
    public float level1MaxTime = 10f;
    public float level1MinTime = 5f;
    public int level1MonsterCount = 25;
    public GameObject level1Indicator;

    [Header("Level 2")]
    public GameObject[] monstersLevel2;
    public float level2MaxTime = 7.5f;
    public float level2MinTime = 4f;
    public int level2MonsterCount = 25;
    public GameObject level2Indicator;

    [Header("Level 3")]
    public GameObject[] monstersLevel3;
    public float level3MaxTime = 6f;
    public float level3MinTime = 3f;
    public int level3MonsterCount = 25;
    public GameObject level3Indicator;

    [Header("Level Boss")]
    public GameObject monsterLevelBoss;
    public GameObject[] monstersLevelBoss;
    public float levelBossMaxTime = 6f;
    public float levelBossMinTime = 3f;
    public int levelBossMonsterCount = 25;
    public GameObject levelBossIndicator;

    private GameObject[] monsters;

    private float spawnTime = 1f;
    private float maxTime = 10f;
    private float minTime = 5f;
    private float time = 0;
    private int monsterCount = 0;
    private int maxMonsterCount = 0;
    
    private float difficultyIncreaseTime=60f;
    private float currentDifficultyIncreaseTime = 30f;
    private int amountOfEnemies;
    private bool isBossSpawned = false;

    bool isLevel1Completed = false;
    bool isLevel2Completed = false;
    bool isLevel3Completed = false;
    bool isLevelBossCompleted = false;

    private int secondsToWait = 3;

    private Dictionary<GameObject, Vector3> MonsterToSpawnOffsetMap;

    void Start () {
        //Sets when the monster should first spawn
        spawnTime = 0; //TODO: Random.Range(minTime, maxTime);
        Cursor.visible = false;

        monsters = monstersLevel1;
        monsterCount = 0;
        maxMonsterCount = level1MonsterCount;

        amountOfEnemies = monsters.Length;
        MonsterToSpawnOffsetMap = Monsters.createMonsterToSpawnOffsetMap();

        BGM[0].Play();
        level1Indicator.SetActive(true);
        Wait(secondsToWait);
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
        if (time >= spawnTime && monsterCount < maxMonsterCount)
        {
            //Resets timer
            time = 0;
            spawnTime = Random.Range(minTime, maxTime);

            //Randomly picks monster to spawn and spawns it
            int chosenMonster = Random.Range(0, amountOfEnemies);
            
            SpawnMonster(monsters[chosenMonster]);
            monsterCount++;
        }
        if (monsterCount >= maxMonsterCount && GameObject.FindGameObjectsWithTag("Monster").Length <= 0)
        {
            time = 0;
            monsterCount = 0;
            if (!isLevel1Completed)
            {
                isLevel1Completed = true;
                maxMonsterCount = level2MonsterCount;
                minTime = level2MinTime;
                maxTime = level2MaxTime;
                monsters = monstersLevel2;

                stopOtherMusic(1);
                BGM[1].Play();
                
                level2Indicator.SetActive(true);
                Wait(secondsToWait);
            }
            else if (!isLevel2Completed)
            {
                isLevel2Completed = true;
                maxMonsterCount = level3MonsterCount;
                minTime = level3MinTime;
                maxTime = level3MaxTime;
                monsters = monstersLevel3;

                stopOtherMusic(2);
                BGM[2].Play();

                level3Indicator.SetActive(true);
                Wait(secondsToWait);
            }
            else if (!isLevel3Completed)
            {
                minTime = levelBossMinTime;
                maxTime = levelBossMaxTime;
                isLevel3Completed = true;
                maxMonsterCount = levelBossMonsterCount;
                monsters = monstersLevelBoss;

                stopOtherMusic(3);
                BGM[3].Play();

                levelBossIndicator.SetActive(true);
                Wait(secondsToWait);

                Vector3 position;
                GetSpawnPoint(out position, monsterLevelBoss);
                position.x = 0;

                GameObject newMonster = GameObject.Instantiate(monsterLevelBoss, position, Quaternion.identity);
                newMonster.SetActive(true);
            }
            else if (!isLevelBossCompleted)
            {
                isLevelBossCompleted = true;
                // call next stage
                SceneManager.LoadScene("GameStart");
            }
            amountOfEnemies = monsters.Length;
        }
    }

    public void Wait(float seconds)
    {
        StartCoroutine(_wait(seconds));
    }
    IEnumerator _wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void stopOtherMusic (int index)
    {
        for (int i = 0; i < BGM.Length; i ++) {
            if (i != index) BGM[i].Stop();
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
