using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

	// Rotate the object around its local Z axis at 400 degree per second
	void Update () {
		transform.Rotate(new Vector3(0,0,400) * Time.deltaTime);
	}
}
