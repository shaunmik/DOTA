using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostWhite : Ghost
{
    protected override void monsterInit() {
        MonsterBehavior.FollowStandingTargetStart(agent, target.transform.position);
    }
    // TODO: resistances
}
