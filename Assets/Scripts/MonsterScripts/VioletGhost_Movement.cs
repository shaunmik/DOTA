using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VioletGhost_Movement : Monster
{
    public GameObject target;
    private PlayerHealthController playerHealthController;
    public int MonsterAttackDamage = 20;
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
        if (isDead()) return;

        transform.localPosition += transform.forward * speed * Time.deltaTime;

        //Destroys monster once it reaches player for memory management purposes
        if (transform.localPosition.z < target.transform.position.z - 2)
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
