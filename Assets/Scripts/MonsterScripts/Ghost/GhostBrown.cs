using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBrown : Ghost {
    protected Vector3 dest;

	protected override void monsterInit() {
    	dest = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
        MonsterBehavior.FollowStandingTargetStart(agent, dest);
    }

    protected override bool playerDamageCriteria()
    {
       return (transform.position.z <= target.transform.position.z + 15f && agent.remainingDistance < 2f);
    }

}
