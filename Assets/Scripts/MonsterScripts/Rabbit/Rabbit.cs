using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rabbit : Monster
{
    public float wanderRadius = 50;
    protected float minDestChangeRate = 2;
    protected float maxDestChangeRate = 3;
    protected float nextDestChange = 0;
    protected int mask;

    protected float targetPlayerDist = 70;
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
            if (Vector3.Distance(transform.position, closestCastlePos) < targetPlayerDist) {
                MonsterBehavior.FollowStandingTargetStart(agent, target.transform.position);
                targetPlayer = true;
            } else if (!staggered) {
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
