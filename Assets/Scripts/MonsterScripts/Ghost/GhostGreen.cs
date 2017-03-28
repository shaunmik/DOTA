using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGreen : Ghost 
{

    protected override void monsterInit() {
    	Vector3 dest = target.transform.position;
        MonsterBehavior.FollowStandingTargetStart(agent, dest);
    }

}
