using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteGhost_Movement : Monster
{
    public GameObject target;
    private PlayerHealthController playerHealthController;
    public int MonsterAttackDamage = 10;
    public float speed = 25f;
    MonsterHandler monsterHandler;

    // Use this for initialization
    void Start()
    {
        InitializeRotation();
        playerHealthController = FindObjectOfType<PlayerHealthController>();
        monsterHandler = GetComponent<MonsterHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead()) return; // so that the object does not move

        LookAt(target.transform.position - transform.position);
        //Moves monster towards target
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, target.transform.position.y - 5, target.transform.position.z), speed * Time.deltaTime);

        //Destroys monster once it reaches player for memory management purposes
        if (transform.localPosition.x == target.transform.position.x && transform.localPosition.z == target.transform.position.z)
        {
            // Damage the player.
            playerHealthController.TakeDamage(MonsterAttackDamage);
            // Destroy the monster.
            Destroy(this.gameObject);
        }
    }

    bool isDead()
    {
        return monsterHandler.isDead();
    }
}
