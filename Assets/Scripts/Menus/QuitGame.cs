using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{

    private float StartingLoadLevel = 0f;
    public Image Quit;
    private float CurrentLoadLevel;
    private bool loaded;

    void Start()
    {
        loaded = false;
        CurrentLoadLevel = StartingLoadLevel;
        Quit.fillAmount = 0f;
    }
    public bool load()
    {
        if (loaded)
            return loaded;
        CurrentLoadLevel += 0.01f;
        Quit.fillAmount = CurrentLoadLevel;
        if (CurrentLoadLevel >= 1f)
        {
            loaded = true;
        }
        return loaded;
    }
    public void unload()
    {
        CurrentLoadLevel = StartingLoadLevel;
        Quit.fillAmount = CurrentLoadLevel;
        loaded = false;
    }
}
