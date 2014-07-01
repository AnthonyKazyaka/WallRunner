using UnityEngine;
using System.Collections;

public interface IButton
{
    KeyCode ButtonCode { get; set; }

    bool IsPressed { get; }
    bool IsHeld { get; }
    bool IsReleased { get; }
}
