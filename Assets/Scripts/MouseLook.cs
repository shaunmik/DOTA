﻿using UnityEngine;

public class MouseLook : MonoBehaviour {

// Modified version of code from 
// https://forums.oculus.com/vip/discussion/27747/mouse-look-during-development

#if UNITY_EDITOR

private float mouseX = 0;
private float mouseY = 0;
private float mouseZ = 0;

public bool mouseLookEnabled = false;
public bool pitchYawEnabled = false;
public bool enableYaw = true;
public bool autoRecenterPitch = false;
public bool autoRecenterRoll = true;

	// Update is called once per frame
	void Update () {
		if (mouseLookEnabled)
		{
			bool rolled = false;
			bool pitched = false;

			if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) {
				pitchYawEnabled = !pitchYawEnabled;
				if (!pitchYawEnabled && !pitched && autoRecenterPitch) {
					// People don't usually leave their heads tilted to one side for long.
					mouseY = Mathf.Lerp(mouseY, 0, Time.deltaTime / (Time.deltaTime + 0.1f));
				}
			} else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
				rolled = true;
				mouseZ += Input.GetAxis("Mouse X") * 5;
				mouseZ = Mathf.Clamp(mouseZ, -85, 85);
			}
			if (!rolled && autoRecenterRoll) {
			// People don't usually leave their heads tilted to one side for long.
			mouseZ = Mathf.Lerp(mouseZ, 0, Time.deltaTime / (Time.deltaTime + 0.1f));
			}

			if (pitchYawEnabled) {
				pitched = true;
				if (enableYaw) {
					mouseX += Input.GetAxis("Mouse X") * 5;
					if (mouseX <= -180) {
						mouseX += 360;
						} else if (mouseX > 180) {
							mouseX -= 360;
						}
				}
				mouseY -= Input.GetAxis("Mouse Y") * 2.4f;
				mouseY = Mathf.Clamp(mouseY, -85, 85);
			}
			transform.localRotation = Quaternion.Euler(mouseY, mouseX, mouseZ);
		}
	}

#endif
}
