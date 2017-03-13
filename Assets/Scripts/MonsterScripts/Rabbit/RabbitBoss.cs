using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBoss : Rabbit 
{
	protected override void monsterInit() {
        targetPlayerDist = 130;
        base.monsterInit();
    }

	protected override bool playerDamageCriteria()
    {
       return (transform.position.z <= target.transform.position.z + 60f && agent.remainingDistance < 60f);
    }
}
