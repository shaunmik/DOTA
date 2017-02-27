using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWhite : Monster
{
    protected override void lookAtTarget()
    {
        LookAt(target.transform.position - transform.position);
    }

    protected override void monsterMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, target.transform.position.y - 5, target.transform.position.z), speed * Time.deltaTime);
    }

    protected override bool playerDamageCriteria()
    {
        return (transform.localPosition.x == target.transform.position.x && transform.localPosition.z == target.transform.position.z);
    }
}
