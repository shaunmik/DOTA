using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuVRLook : MonoBehaviour
{

    private float StartingLoadLevel = 0f;
    public Image FillImage;
    private float CurrentLoadLevel;
    private bool loaded;

    void Start()
    {
        loaded = false;
        CurrentLoadLevel = StartingLoadLevel;
        FillImage.fillAmount = 0f;
    }
    public bool load()
    {
        if (loaded)
            return loaded;
        CurrentLoadLevel += 0.01f;
        FillImage.fillAmount = CurrentLoadLevel;
        if (CurrentLoadLevel >= 1f)
        {
            loaded = true;
        }
        return loaded;
    }
    public void unload()
    {
        CurrentLoadLevel = StartingLoadLevel;
        FillImage.fillAmount = CurrentLoadLevel;
        loaded = false;
    }
}
