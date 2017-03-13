using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : Monster {

	public static Vector3 spawnOffset = Vector3.up * 3.3f;

    protected override void monsterInit() {
        // overridden
    }

    protected override void monsterMovement()
    {
        // Do nothing
    }

    protected override bool playerDamageCriteria()
    {
       return (transform.position.z <= target.transform.position.z);
    }

    public static Vector3 getSpawnOffset() {
    	return spawnOffset;
    }

}
