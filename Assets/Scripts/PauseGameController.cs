using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameController : MonoBehaviour
{

    private GameObject gui;
    public GameObject pausemenu;
    public GameObject VRCameraUI;

    private RaycastManager raycastManager;

    void Start()
    {
        raycastManager = FindObjectOfType<RaycastManager>();
        gui = GameObject.Find("GUI");
    }

    private SteamVR_TrackedObject LeftTrackedObj;
    private SteamVR_TrackedObject RightTrackedObj;
    // 2
    private SteamVR_Controller.Device LeftController
    {
        get
        {
            return SteamVR_Controller.Input((int)LeftTrackedObj.index);
        }
    }
    private SteamVR_Controller.Device RightController
    {
        get
        {
            return SteamVR_Controller.Input((int)RightTrackedObj.index);
        }
    }

    private void Awake()
    {
        LeftTrackedObj = FindObjectsOfType<SteamVR_TrackedObject>()[0];
        RightTrackedObj = FindObjectsOfType<SteamVR_TrackedObject>()[1];
    }

    void Update()
    {

        if (ActionControlListener.isStartButtonPressed() || LeftController.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) || RightController.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            if (Time.timeScale == 1)
            {
                pause();
            }
            else
            {
                resume();
            }
        }

    }

    public void pause()
    {
        Time.timeScale = 0;
        gui.SetActive(false);
        pausemenu.SetActive(true);
        VRCameraUI.SetActive(true);
    }

    public void resume()
    {
        Time.timeScale = 1;
        gui.SetActive(true);
        pausemenu.SetActive(false);
        VRCameraUI.SetActive(false);
    }

    public void inactivatePauseMenu()
    {
        pausemenu.SetActive(false);
        VRCameraUI.SetActive(false);
    }
}
