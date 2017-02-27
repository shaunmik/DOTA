using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealthController : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LookAt(target.transform.position - transform.position);
    }

    protected void LookAt(Vector3 direction)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
