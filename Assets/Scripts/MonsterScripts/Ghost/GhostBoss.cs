using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostBoss : GhostWhite
{
    protected override void monsterInit() {
        MonsterBehavior.FollowStandingTargetStart(agent, target.transform.position);
    }

    protected override bool playerDamageCriteria()
    {
       return (transform.position.z <= target.transform.position.z + 70f && agent.remainingDistance < 70f);
    }
}
