using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

abstract public class Monster : MonoBehaviour
{
    public GameObject target;
    protected NavMeshAgent agent;
    protected Animator anim;
    protected PlayerHealthController playerHealthController;

    public float fireResistance = 0f;
    public float waterResistance = 0f;
    public float windResistance = 0f;
    public float earthResistance = 0f;

    public float MonsterStartHealth = 100;
    private float MonsterHealth;

    public int MonsterAttackDamage = 10;

    public Vector3 translation = Vector3.zero;

    [Header("Unity Stuff")]
    public Image HealthBar;

    private bool dead = false;
    protected bool staggered = false;  // Whether this monster is currently staggered; 
                                       // Used to prevent new destination to be assigned to agent
    private float originalSpeed;
    private GameManager gameManager;
    private float resumeTime;
    private int staggerThreshold = 100;  // Damage above or equal to this number 
                                         // will stagger the monster
    
    

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
        if (!dead && playerDamageCriteria())
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
                dead = true;
                
                if (gameManager != null) {
                    // increament the score
                    gameManager.addScore(1);
                }

                // Stop the monster from moving
                agent.enabled = false;

                // Destroy the monster with/without animation
                if (anim != null) {
                    destroySelf();
                } else {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void applySpeedChange(float speedMultiplier)
    {
        agent.speed *= speedMultiplier;
    }

    public void resetSpeed()
    {
        agent.speed = originalSpeed;
    }

    public float damageMultiplier (Elements.elemEnum element)
    {
        if (element == Elements.elemEnum.fire)
        {
            return (100f - fireResistance) / 100f;
        }
        else if (element == Elements.elemEnum.water)
        {
            return (100f - waterResistance) / 100f;
        }
        else if (element == Elements.elemEnum.earth)
        {
            return (100f - fireResistance) / 100f;
        }
        else if (element == Elements.elemEnum.wind)
        {
            return (100f - windResistance) / 100f;
        }
        else
        {
            return 1f;
        }
    }
    public void takeDamage(int damage, Elements.elemEnum firstElement, Elements.elemEnum secondElement)
    {
        MonsterHealth -= damage*damageMultiplier(firstElement)*damageMultiplier(secondElement);

        HealthBar.fillAmount = MonsterHealth / MonsterStartHealth;

        checkDead();

        // Animate the damage taking and stagger according to damage threshold
        if (!dead && anim != null) {
            anim.SetTrigger("TakeDamage");
            if (damage >= staggerThreshold) {
                agent.Stop();
                staggered = true;
                StartCoroutine(resumeMovementPostAnimation());
            }
        }
    }

    IEnumerator resumeMovementPostAnimation()
    {
        resumeTime = Time.time + 0.5f;
        yield return new WaitForSeconds(0.5f);
        if (Time.time >= resumeTime) {
            agent.Resume();
            staggered = false;
        }
    }

    protected void damagePlayer()
    {
        // Damage the player
        playerHealthController.TakeDamage(MonsterAttackDamage);

        // Denote that despawning sequence initiated
        dead = true;

        // Play the attack animation and clean up
        StartCoroutine(playAttackAnimation());
    }

    IEnumerator playAttackAnimation()
    {
        if (anim != null) {
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(1.0f);
        }
        destroySelf();
    }

    protected abstract void monsterInit();

    protected abstract void monsterMovement();

    protected abstract bool playerDamageCriteria();

}
