using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostViolet : Monster
{

    protected override void monsterInit() {
        MonsterBehavior.FollowStandingTargetStart(agent, target);
    }

    protected override void monsterMovement()
    {
        // Do nothing
    }

    protected override bool playerDamageCriteria()
    {
       return (transform.localPosition.z < target.transform.position.z);
    }

    protected override void lookAtTarget()
    {
        // Don't even need this
    }
    
}
