using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Monster {

    protected override void monsterInit() {
        MonsterBehavior.FollowStandingTargetStart(agent, target);
    }

    protected override void monsterMovement()
    {
        throw new NotImplementedException();
    }

    protected override bool playerDamageCriteria()
    {
        throw new NotImplementedException();
    }
}
