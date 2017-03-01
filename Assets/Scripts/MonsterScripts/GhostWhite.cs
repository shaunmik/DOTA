using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostWhite : Monster
{

    protected override void monsterInit() {
        MonsterBehavior.FollowStandingTargetStart(agent, target);
    }


    protected override void lookAtTarget()
    {
        // Do nothing
    }

    protected override void monsterMovement()
    {
        // Do nothing
    }

    protected override bool playerDamageCriteria()
    {
        return (transform.localPosition.x == target.transform.position.x && transform.localPosition.z == target.transform.position.z);
    }
    
}
