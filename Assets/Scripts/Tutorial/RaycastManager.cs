using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour {

    public Camera cam;
    int cubeCount = 0;

    Ray ray;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast (rayOrigin,cam.transform.forward, out hit, 300f))
        {
            if (hit.collider.transform.parent.name.Equals("PauseMenu")) { return; }
            Destroy(hit.collider.gameObject);
            cubeCount++;
        }

        if (cubeCount > 5)
        {
            TutorialManager manager = this.transform.GetComponent<TutorialManager>();
            manager.setTrainingCubeCompleted(true);
            this.enabled = false;
        }
    }
}
