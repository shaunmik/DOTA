using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGreen : Ghost {
    protected override void monsterInit()
    {
        MonsterBehavior.FollowStandingTargetStart(agent, target.transform.position);
    }
    protected override void monsterMovement()
    {
        agent.speed = agent.speed * 1.15f;
    }
}
