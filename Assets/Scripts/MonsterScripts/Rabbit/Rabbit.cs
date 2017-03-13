using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rabbit : Monster
{
    public float wanderRadius = 50;
    public float minDestChangeRate = 4;
    public float maxDestChangeRate = 6;

    public float nextDestChange = 0;
    public int mask;

    private bool targetPlayer = false;

    protected override void monsterInit() {
        mask = NavMesh.GetAreaFromName("Walkable");
        Wander();
    }

    protected override void monsterMovement()
    {
        if (!targetPlayer) {
            // If rabbit is 70 distance from entrance, make it go there.
            // Otherwise, let it wander a bit.
            Vector3 closestCastlePos = new Vector3(transform.position.x, 
                                                   transform.position.y, 
                                                   target.transform.position.z);
            if (Vector3.Distance(transform.position, closestCastlePos) < 70) {
                MonsterBehavior.FollowStandingTargetStart(agent, target);
                targetPlayer = true;
                Debug.Log("targetPlayer");
            } else {
                Wander();
            }
        }

        // Update animator parameter to denote movement
        if (Vector3.Distance(agent.velocity, Vector3.zero) != 0.0) {
            anim.SetBool("Moving", true);
        } else {
            anim.SetBool("Moving", false);
        }
    }

    protected override bool playerDamageCriteria()
    {
       return (transform.position.z <= target.transform.position.z);
    }

    protected void Wander() 
    {
        float destChangeRate = UnityEngine.Random.Range(minDestChangeRate, maxDestChangeRate);

        MonsterBehavior.Wander(agent, gameObject, mask, wanderRadius, destChangeRate, ref nextDestChange);
    }
    
}
