using UnityEngine;
using System.Collections;

public class Button : IButton
{
    public KeyCode ButtonCode { get; set; }

    public bool IsPressed
    {
        get { return IsButtonPressed(); }
    }

    public bool IsHeld
    {
        get { return IsButtonHeld(); }
    }

    public bool IsReleased
    {
        get { return IsButtonReleased(); }
    }


    public Button(KeyCode keyCode)
    {
        ButtonCode = keyCode;
    }

    private bool IsButtonPressed()
    {
        return Input.GetKeyDown(ButtonCode);
    }

    private bool IsButtonHeld()
    {
        return Input.GetKey(ButtonCode);
    }

    private bool IsButtonReleased()
    {
        return Input.GetKeyUp(ButtonCode);
    }
}
