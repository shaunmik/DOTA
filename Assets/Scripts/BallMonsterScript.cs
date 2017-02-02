using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMonsterScript : MonoBehaviour {
    public GameObject target;
    private PlayerHealthController playerHealthController;
    private int PlayerDamage = 20; 
    private float speed = 25f;
    // Use this for initialization
    void Start () {
	  playerHealthController = FindObjectOfType<PlayerHealthController>();	
	}
	
	// Update is called once per frame
	void Update () {
        //Moves monster towards target
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.localScale.y, target.transform.position.z), speed * Time.deltaTime);

        //Destroys monster once it reaches player for memory management purposes
        if (transform.localPosition.x == target.transform.position.x && transform.localPosition.z == target.transform.position.z)
        {
            // Damage the player.
            playerHealthController.TakeDamage(PlayerDamage);
            // Destroy the monster.
            Destroy(this.gameObject);
        }
    }
}
