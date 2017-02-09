using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    public GameObject ballMonster;
    public GameObject directMovementMonster;
    public float spawnTime = 1f;
    private float maxTime = 2f;
    private float minTime = 1f;
    private float time = 0;
    private int amountOfEnemies=2;

    // Use this for initialization
    void Start () {
        //Sets when the monster should first spawn
        spawnTime = Random.Range(minTime, maxTime);
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

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
                SpawnBallMonster();
            }
            if (chosenMonster == 1)
            {
                SpawnDirectMovementMonster();
            }
        }
    }

    //Spawns a ball monster
    void SpawnBallMonster()
    {
        
        //Randomizes position of monster
        int spawnPositionX = Random.Range(-75, 75);
        var position = new Vector3(spawnPositionX, 5f, 160f);

        //Creates monster
        var newBallMonster = GameObject.Instantiate(ballMonster, position, Quaternion.identity);
        newBallMonster.SetActive(true);
    }

    void SpawnDirectMovementMonster()
    {
        //Randomizes position of monster
        int spawnPositionX = Random.Range(-75, 75);
        var position = new Vector3(spawnPositionX, 5f, 160f);

        //Creates monster
        var newDirectMovementMonster = GameObject.Instantiate(directMovementMonster, position, Quaternion.identity);
        newDirectMovementMonster.SetActive(true);
    }
}
