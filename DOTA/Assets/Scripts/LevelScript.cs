using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    public GameObject ballMonster;
    public float spawnTime = 1f;
    private float maxTime = 2f;
    private float minTime = 1f;
    private float time = 0;

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
            SpawnBallMonster();
            spawnTime = Random.Range(minTime, maxTime);
        }
    }

    //Spawns a ball monster
    void SpawnBallMonster()
    {
        time = 0;
        //Randomizes position of monster
        int spawnPositionX = Random.Range(-75, 75);
        var position = new Vector3(spawnPositionX, 5f, 160f);

        //Creates monster
        var newBallMonster = GameObject.Instantiate(ballMonster, position, Quaternion.identity);
        newBallMonster.SetActive(true);
    }
}
