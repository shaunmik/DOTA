using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostViolet : Ghost
{
    protected override void monsterInit() {
    	Vector3 dest = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
        MonsterBehavior.FollowStandingTargetStart(agent, dest);
    }
    // TODO: resistances
}
