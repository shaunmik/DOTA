using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

abstract public class Monster : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    protected Animator anim;
    protected PlayerHealthController playerHealthController;

    public float MonsterStartHealth = 100;
    private float MonsterHealth;

    public int MonsterAttackDamage = 10;

    public Vector3 translation = Vector3.zero;

    [Header("Unity Stuff")]
    public Image HealthBar;

    private bool dead = false;
    private bool despawn = false;
    private float originalSpeed;
    private GameManager gameManager;
    private float resumeTime;

    protected bool paused = false;
    

    // Use this for initialization
    protected void Start()
    {
        agent = GetComponent <NavMeshAgent> ();
        anim = GetComponent<Animator>();
        monsterInit();
        
        gameManager = FindObjectOfType<GameManager>();
        InitializeRotation();
        playerHealthController = intitializePlayerHealthController();
        MonsterHealth = MonsterStartHealth;

        originalSpeed = agent.speed;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (isDead()) return; // so that the object does not move

        //Moves monster towards target
        monsterMovement();

        //Destroys monster once it reaches player for memory management purposes
        if (!despawn && playerDamageCriteria())
        {
            damagePlayer();
        }
    }

    protected PlayerHealthController intitializePlayerHealthController()
    {
        return FindObjectOfType<PlayerHealthController>();
    }

    protected bool isDead()
    {
        return dead;
    }

    protected void InitializeRotation()
    {
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }

    protected void LookAt(Vector3 direction)
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
        if (anim != null) {
            anim.SetTrigger("Die");
            yield return new WaitForSeconds(1.5f);
        }
        Destroy(this.gameObject);
    }

    protected void checkDead()
    {
        if (MonsterHealth <= 0)
        {
            if (!dead) {
                if (gameManager != null) {
                    // increament the score
                    gameManager.addScore(1);
                }
                dead = true;
                // Stop the monster from moving
                agent.Stop();
            }

            if (anim != null && !despawn) {
                despawn = true;
                destroySelf();
            } else {
                Destroy(this.gameObject);
            }

        }
    }

    public void applySpeedChange(float speedMultiplier, Elements.elemEnum elementType)
    {
        agent.speed *= speedMultiplier;
    }

    public void resetSpeed()
    {
        agent.speed = originalSpeed;
    }

    public void takeDamage(int damage)
    {
        MonsterHealth -= damage;

        HealthBar.fillAmount = MonsterHealth / MonsterStartHealth;

        checkDead();

        // Animate the damage taking
        if (!dead && anim != null) {
            agent.Stop();
            paused = true;
            // agent.enabled = false;
            anim.SetTrigger("TakeDamage");
            StartCoroutine(resumeMovementPostAnimation());
        }
    }

    IEnumerator resumeMovementPostAnimation()
    {
        resumeTime = Time.time + 0.5f;
        yield return new WaitForSeconds(0.5f);
        if (Time.time >= resumeTime) {
            agent.Resume();
            paused = false;
            // agent.enabled = true;
        }
    }

    protected void damagePlayer()
    {
        // Damage the player.
        playerHealthController.TakeDamage(MonsterAttackDamage);

        despawn = true;
        // Play the attack animation and clean up
        StartCoroutine(playAttackAnimation());
    }

    IEnumerator playAttackAnimation()
    {
        if (anim != null) {
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.9f);
        }
        destroySelf();
    }

    protected abstract void monsterInit();

    protected abstract void monsterMovement();

    protected abstract bool playerDamageCriteria();

}
