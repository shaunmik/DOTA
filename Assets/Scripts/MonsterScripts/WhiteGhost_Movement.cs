using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteGhost_Movement : Monster
{
    public GameObject target;
    private PlayerHealthController playerHealthController;
    public int Health = 20;
    public int PlayerDamage = 20;
    public float speed = 25f;
    // Use this for initialization
    void Start()
    {
        InitializeRotation();
        playerHealthController = FindObjectOfType<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAt(target.transform.position - transform.position);
        //Moves monster towards target
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, target.transform.position.y - 5, target.transform.position.z), speed * Time.deltaTime);

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
