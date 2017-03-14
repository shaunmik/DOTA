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

    void Update()
    {
        if (ActionControlListener.isStartButtonPressed())
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
