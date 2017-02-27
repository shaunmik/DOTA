using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostViolet : Monster
{

    protected override void monsterMovement()
    {
        transform.localPosition += transform.forward * speed * Time.deltaTime;
    }

    protected override bool playerDamageCriteria()
    {
       return (transform.localPosition.z < target.transform.position.z);
    }

    protected override void lookAtTarget()
    {
        throw new NotImplementedException();
    }
}
