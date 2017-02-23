using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {

    public Camera cam;
    private Vector3 originalScale;
	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        float distance;
		if (Physics.Raycast(new Ray(cam.transform.position, cam.transform.rotation * Vector3.forward), out hit))
        {
            distance = hit.distance;
        } else
        {
            distance = cam.farClipPlane * 0.95f;
        }

        transform.position = cam.transform.position + cam.transform.rotation * Vector3.forward * distance;
        transform.LookAt(cam.transform.position);
        transform.Rotate(0f, 180f, 0f);
        transform.rotation = cam.transform.rotation;
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, cam.transform.rotation.x);
        transform.localScale = originalScale * distance;
	}
}
