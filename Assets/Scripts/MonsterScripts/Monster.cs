using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Monster : MonoBehaviour {

    public void InitializeRotation()
    {
        transform.rotation = new Quaternion(0, 180, 0, 0);
    } 

    public void LookAt (Vector3 direction)
    {
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }

    public void Destroy()
    {
        //TODO add death animation here
    }
}
