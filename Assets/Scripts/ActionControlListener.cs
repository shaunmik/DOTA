using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Action Controller for XBox 360 and Keyboard
 * 
 * Fire | Water | Earth | Wind | Left Confirm | Right Confirm | Start | Back | Left Trigger | Right Trigger
 *   Q      A       S       W           1               2       Enter    Esc     Lft Mouse      Rgt Mouse
 *   X      A       B       Y           -               -        -        -         -               -           
 *   
 *   Example usage:
 *           if (ActionControlListener.isFireButtonPressed())
 *              Debug.Log("Fire");
 */


public static class ActionControlListener
{
    
    // Main joystick
    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("360_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("360_MainVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static Vector3 MainJoystick()
    {
        return new Vector3(MainHorizontal(), 0, MainVertical());
    }

    /*
     *  Add Main joystick controls here
     */

    // Left hand buttons
    public static bool isWaterButtonPressed()
    {
        return Input.GetButtonDown("X_Button");
    }
    public static bool isEarthButtonPressed()
    {
        return Input.GetButtonDown("Y_Button");
    }
    public static bool isLeftConfirmPressed()
    {
        return Input.GetButtonDown("Left_Bumper"); 
    }
    public static bool isBackButtonPressed()
    {
        return Input.GetButtonDown("Back_Button");
    }
    /// <summary>
    /// The return value is 1 when trigger is fully pressed down </summary>
    public static float LeftTrigger()
    {
        float r = 0.0f;
        r += Input.GetAxis("Left_Trigger");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static bool isLeftTriggerPressed()
    {
        return LeftTrigger() > 0.0f || Input.GetMouseButtonDown(0);
    }
    public static bool isLeftTriggerFullyPressed()
    {
        return LeftTrigger() == 1 || Input.GetMouseButtonDown(0);
    }

    // Right hand buttons
    public static bool isFireButtonPressed()
    {
        return Input.GetButtonDown("B_Button");
    }
    public static bool isWindButtonPressed()
    {
        return Input.GetButtonDown("A_Button");
    }
    public static bool isRightConfirmPressed()
    {
        return Input.GetButtonDown("Right_Bumper");
    }
    public static bool isStartButtonPressed()
    {
        return Input.GetButtonDown("Start_Button");
    }
   
    /// <summary>
    /// The return value is 1 when trigger is fully pressed down </summary>
    public static float RightTrigger()
    {
        float r = 0.0f;
        r += Input.GetAxis("Right_Trigger");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static bool isRightTriggerPressed()
    {
        return RightTrigger() > 0.0f || Input.GetMouseButtonDown(1); 
    }
    public static bool isRightTriggerFullyPressed()
    {
        return RightTrigger() == 1 || Input.GetMouseButtonDown(1); 
    }
}
