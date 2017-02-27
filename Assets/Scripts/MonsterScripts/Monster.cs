using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Monster : MonoBehaviour {
    public GameObject target;
    public int MonsterHealth = 100;
    public int MonsterAttackDamage = 10;
    public float speed = 25f;

    private bool dead = false;

    protected PlayerHealthController playerHealthController;

    // Use this for initialization
    protected void Start()
    {
        InitializeRotation();
        playerHealthController = intitializePlayerHealthController();
    }

    // Update is called once per frame
    protected void Update()
    {
        checkDead();
        if (isDead()) return; // so that the object does not move

        try {
            lookAtTarget();
        }
        catch { } // this will ignore the unimplemented exception

        //Moves monster towards target
        monsterMovement();

        //Destroys monster once it reaches player for memory management purposes
        if (playerDamageCriteria())
        {
            damagePlayer();
        }
    }

    protected PlayerHealthController intitializePlayerHealthController ()
    {
        return FindObjectOfType<PlayerHealthController>();
    }

    protected bool isDead()
    {
        return dead;
    }

    protected void playDead()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("ghost_die", 0);
    }

    protected void InitializeRotation()
    {
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }

    protected void LookAt (Vector3 direction)
    {
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }

    protected void destroySelf()
    {
        StartCoroutine(playDeathAnimation());
    }

    IEnumerator playDeathAnimation()
    {
        playDead();
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    protected void checkDead()
    {
        if (MonsterHealth <= 0)
        {
            dead = true;
            destroySelf();
        }
    }

    public void takeDamage(int damage)
    {
        MonsterHealth = MonsterHealth - damage;
    }

    protected void damagePlayer()
    {
        // Damage the player.
        playerHealthController.TakeDamage(MonsterAttackDamage);
        // Destroy the monster.
        Destroy(this.gameObject);
    }
    protected abstract void monsterMovement();

    protected abstract bool playerDamageCriteria();

    protected abstract void lookAtTarget();
}
