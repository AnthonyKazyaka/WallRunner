using UnityEngine;
using System.Collections;

public class Xbox360Controller : Controller
{

    // In the input settings file, the axes for getting input from any controller is controller 0. Otherwise,
    // you should use the player number explicitly.
    private static Xbox360Controller _anyController = new Xbox360Controller(PlayerNumbers.Any);
    public static Xbox360Controller AnyController {get { return _anyController; }}

    private static Xbox360Controller _player1Controller = new Xbox360Controller(PlayerNumbers.Player1);
    public static Xbox360Controller Player1Contoller {get { return _player1Controller; }}

    private static Xbox360Controller _player2Controller = new Xbox360Controller(PlayerNumbers.Player2);
    public static Xbox360Controller Player2Controller {get { return _player2Controller; }}

    private static Xbox360Controller _player3Controller = new Xbox360Controller(PlayerNumbers.Player3);
    public static Xbox360Controller Player3Controller {get { return _player3Controller; }}

    private static Xbox360Controller _player4Controller = new Xbox360Controller(PlayerNumbers.Player4);
    public static Xbox360Controller Player4Controller {get { return _player4Controller; }}

    public enum ButtonState
    {
        Pressed,
        Held,
        Released
    }

    public Button A { get; private set; }
    public Button B { get; private set; }
    public Button X { get; private set; }
    public Button Y { get; private set; }
    public Button Start { get; private set; }
    public Button Back { get; private set; }
    public Button LeftThumbstickButton { get; private set; }
    public Button RightThumbstickButton { get; private set; }
    public Button RightBumper { get; private set; }
    public Button LeftBumper { get; private set; }
          
    public float LeftThumbstickX {get { return GetLeftThumbstickX(); }}
    public float LeftThumbstickY {get { return GetLeftThumbstickY(); }}
    public float RightThumbstickX { get { return GetRightThumbstickX(); } }
    public float RightThumbstickY { get { return GetRightThumbstickY(); } }    
    public float LeftTrigger {get { return GetLeftTriggerValue(); }}
    public float RightTrigger{get { return GetRightTriggerValue(); }}
    public float DPadHorizontal {get { return GetDPadHorizontalValue(); }}
    public float DPadVertical {get { return GetDPadVerticalValue(); }}

    private PlayerNumbers _playerNumber;

    public Xbox360Controller(PlayerNumbers playerNumber) : base(playerNumber)
    {
        _playerNumber = playerNumber;
        switch (_playerNumber)
        {
            case PlayerNumbers.Any:
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
                A = new Button(KeyCode.JoystickButton0);
                B = new Button(KeyCode.JoystickButton1);
                X = new Button(KeyCode.JoystickButton2);
                Y = new Button(KeyCode.JoystickButton3);
                Start = new Button(KeyCode.JoystickButton7);
                Back = new Button(KeyCode.JoystickButton6);
                LeftBumper = new Button(KeyCode.JoystickButton4);
                RightBumper = new Button(KeyCode.JoystickButton5);
                LeftThumbstickButton = new Button(KeyCode.JoystickButton8);
                RightThumbstickButton = new Button(KeyCode.JoystickButton9);
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                A = new Button(KeyCode.JoystickButton16);
                B = new Button(KeyCode.JoystickButton17);
                X = new Button(KeyCode.JoystickButton18);
                Y = new Button(KeyCode.JoystickButton19);
                Start = new Button(KeyCode.JoystickButton9);
                Back = new Button(KeyCode.JoystickButton10);
                LeftBumper = new Button(KeyCode.JoystickButton13);
                RightBumper = new Button(KeyCode.JoystickButton14);
                LeftThumbstickButton = new Button(KeyCode.JoystickButton11);
                RightThumbstickButton = new Button(KeyCode.JoystickButton12);
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
                A = new Button(KeyCode.JoystickButton0);
                B = new Button(KeyCode.JoystickButton1);
                X = new Button(KeyCode.JoystickButton2);
                Y = new Button(KeyCode.JoystickButton3);
                Start = new Button(KeyCode.JoystickButton7);
                Back = new Button(KeyCode.JoystickButton6);
                LeftBumper = new Button(KeyCode.JoystickButton4);
                RightBumper = new Button(KeyCode.JoystickButton5);
                LeftThumbstickButton = new Button(KeyCode.JoystickButton9);
                RightThumbstickButton = new Button(KeyCode.JoystickButton10);
#endif
                break;
            case PlayerNumbers.Player1:
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
                A = new Button(KeyCode.Joystick1Button0);
                B = new Button(KeyCode.Joystick1Button1);
                X = new Button(KeyCode.Joystick1Button2);
                Y = new Button(KeyCode.Joystick1Button3);
                Start = new Button(KeyCode.Joystick1Button7);
                Back = new Button(KeyCode.Joystick1Button6);
                LeftBumper = new Button(KeyCode.Joystick1Button4);
                RightBumper = new Button(KeyCode.Joystick1Button5);
                LeftThumbstickButton = new Button(KeyCode.Joystick1Button8);
                RightThumbstickButton = new Button(KeyCode.Joystick1Button9);
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                A = new Button(KeyCode.Joystick1Button16);
                B = new Button(KeyCode.Joystick1Button17);
                X = new Button(KeyCode.Joystick1Button18);
                Y = new Button(KeyCode.Joystick1Button19);
                Start = new Button(KeyCode.Joystick1Button9);
                Back = new Button(KeyCode.Joystick1Button10);
                LeftBumper = new Button(KeyCode.Joystick1Button13);
                RightBumper = new Button(KeyCode.Joystick1Button14);
                LeftThumbstickButton = new Button(KeyCode.JoystickButton11);
                RightThumbstickButton = new Button(KeyCode.Joystick1Button12);
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
                A = new Button(KeyCode.Joystick1Button0);
                B = new Button(KeyCode.Joystick1Button1);
                X = new Button(KeyCode.Joystick1Button2);
                Y = new Button(KeyCode.Joystick1Button3);
                Start = new Button(KeyCode.Joystick1Button7);
                Back = new Button(KeyCode.Joystick1Button6);
                LeftBumper = new Button(KeyCode.Joystick1Button4);
                RightBumper = new Button(KeyCode.Joystick1Button5);
                LeftThumbstickButton = new Button(KeyCode.Joystick1Button9);
                RightThumbstickButton = new Button(KeyCode.Joystick1Button10);
#endif
                break;
            case PlayerNumbers.Player2:
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
                A = new Button(KeyCode.Joystick2Button0);
                B = new Button(KeyCode.Joystick2Button1);
                X = new Button(KeyCode.Joystick2Button2);
                Y = new Button(KeyCode.Joystick2Button3);
                Start = new Button(KeyCode.Joystick2Button7);
                Back = new Button(KeyCode.Joystick2Button6);
                LeftBumper = new Button(KeyCode.Joystick2Button4);
                RightBumper = new Button(KeyCode.Joystick2Button5);
                LeftThumbstickButton = new Button(KeyCode.Joystick2Button8);
                RightThumbstickButton = new Button(KeyCode.Joystick2Button9);
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                A = new Button(KeyCode.Joystick2Button16);
                B = new Button(KeyCode.Joystick2Button17);
                X = new Button(KeyCode.Joystick2Button18);
                Y = new Button(KeyCode.Joystick2Button19);
                Start = new Button(KeyCode.Joystick2Button9);
                Back = new Button(KeyCode.Joystick2Button10);
                LeftBumper = new Button(KeyCode.Joystick2Button13);
                RightBumper = new Button(KeyCode.Joystick2Button14);
                LeftThumbstickButton = new Button(KeyCode.Joystick2Button11);
                RightThumbstickButton = new Button(KeyCode.Joystick2Button12);
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
                A = new Button(KeyCode.Joystick2Button0);
                B = new Button(KeyCode.Joystick2Button1);
                X = new Button(KeyCode.Joystick2Button2);
                Y = new Button(KeyCode.Joystick2Button3);
                Start = new Button(KeyCode.Joystick2Button7);
                Back = new Button(KeyCode.Joystick2Button6);
                LeftBumper = new Button(KeyCode.Joystick2Button4);
                RightBumper = new Button(KeyCode.Joystick2Button5);
                LeftThumbstickButton = new Button(KeyCode.Joystick2Button9);
                RightThumbstickButton = new Button(KeyCode.Joystick2Button10);
#endif
                break;
            case PlayerNumbers.Player3:
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
                A = new Button(KeyCode.Joystick3Button0);
                B = new Button(KeyCode.Joystick3Button1);
                X = new Button(KeyCode.Joystick3Button2);
                Y = new Button(KeyCode.Joystick3Button3);
                Start = new Button(KeyCode.Joystick3Button7);
                Back = new Button(KeyCode.Joystick3Button6);
                LeftBumper = new Button(KeyCode.Joystick3Button4);
                RightBumper = new Button(KeyCode.Joystick3Button5);
                LeftThumbstickButton = new Button(KeyCode.Joystick3Button8);
                RightThumbstickButton = new Button(KeyCode.Joystick3Button9);
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                A = new Button(KeyCode.Joystick3Button16);
                B = new Button(KeyCode.Joystick3Button17);
                X = new Button(KeyCode.Joystick3Button18);
                Y = new Button(KeyCode.Joystick3Button19);
                Start = new Button(KeyCode.Joystick3Button9);
                Back = new Button(KeyCode.Joystick3Button10);
                LeftBumper = new Button(KeyCode.Joystick3Button13);
                RightBumper = new Button(KeyCode.Joystick3Button14);
                LeftThumbstickButton = new Button(KeyCode.Joystick3Button11);
                RightThumbstickButton = new Button(KeyCode.Joystick3Button12);
            #endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
                A = new Button(KeyCode.Joystick3Button0);
                B = new Button(KeyCode.Joystick3Button1);
                X = new Button(KeyCode.Joystick3Button2);
                Y = new Button(KeyCode.Joystick3Button3);
                Start = new Button(KeyCode.Joystick3Button7);
                Back = new Button(KeyCode.Joystick3Button6);
                LeftBumper = new Button(KeyCode.Joystick3Button4);
                RightBumper = new Button(KeyCode.Joystick3Button5);
                LeftThumbstickButton = new Button(KeyCode.Joystick3Button9);
                RightThumbstickButton = new Button(KeyCode.Joystick3Button10);
#endif
                break;
            case PlayerNumbers.Player4:
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
                A = new Button(KeyCode.Joystick4Button0);
                B = new Button(KeyCode.Joystick4Button1);
                X = new Button(KeyCode.Joystick4Button2);
                Y = new Button(KeyCode.Joystick4Button3);
                Start = new Button(KeyCode.Joystick4Button7);
                Back = new Button(KeyCode.Joystick4Button6);
                LeftBumper = new Button(KeyCode.Joystick4Button4);
                RightBumper = new Button(KeyCode.Joystick4Button5);
                LeftThumbstickButton = new Button(KeyCode.Joystick4Button8);
                RightThumbstickButton = new Button(KeyCode.Joystick4Button9);
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                A = new Button(KeyCode.Joystick4Button16);
                B = new Button(KeyCode.Joystick4Button17);
                X = new Button(KeyCode.Joystick4Button18);
                Y = new Button(KeyCode.Joystick4Button19);
                Start = new Button(KeyCode.Joystick4Button9);
                Back = new Button(KeyCode.Joystick4Button10);
                LeftBumper = new Button(KeyCode.Joystick4Button13);
                RightBumper = new Button(KeyCode.Joystick4Button14);
                LeftThumbstickButton = new Button(KeyCode.Joystick4Button11);
                RightThumbstickButton = new Button(KeyCode.Joystick4Button12);
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
                A = new Button(KeyCode.Joystick4Button0);
                B = new Button(KeyCode.Joystick4Button1);
                X = new Button(KeyCode.Joystick4Button2);
                Y = new Button(KeyCode.Joystick4Button3);
                Start = new Button(KeyCode.Joystick4Button7);
                Back = new Button(KeyCode.Joystick4Button6);
                LeftBumper = new Button(KeyCode.Joystick4Button4);
                RightBumper = new Button(KeyCode.Joystick4Button5);
                LeftThumbstickButton = new Button(KeyCode.Joystick4Button9);
                RightThumbstickButton = new Button(KeyCode.Joystick4Button10);
#endif
                break;
        }
    }



    private float GetLeftThumbstickX()
    {
        float leftThumbstickX = Input.GetAxis("Controller" + (int)_playerNumber + "LeftThumbstickX");
        return leftThumbstickX;
    }

    private float GetLeftThumbstickY()
    {
        float leftThumbstickX = -Input.GetAxis("Controller" + (int)_playerNumber + "LeftThumbstickY");
		return leftThumbstickX;
    }

    private float GetRightThumbstickX()
    {
        float rightThumbstickX = 0.0f;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        rightThumbstickX = Input.GetAxis("Controller" + (int)_playerNumber + "Axis4");
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        rightThumbstickX = Input.GetAxis("Controller" + (int)_playerNumber + "Axis3");
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
        rightThumbstickX = Input.GetAxis("Controller" + (int)_playerNumber + "Axis4");
#endif
        return rightThumbstickX;
    }

    private float GetRightThumbstickY()
    {
        float rightThumbstickY = 0.0f;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        rightThumbstickY = Input.GetAxis("Controller" + (int)_playerNumber + "Axis5");
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        rightThumbstickY = Input.GetAxis("Controller" + (int)_playerNumber + "Axis4");
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
        rightThumbstickY = Input.GetAxis("Controller" + (int)_playerNumber + "Axis5");
#endif
        return rightThumbstickY;
    }

    private float GetLeftTriggerValue()
    {
        float leftTriggerValue = 0.0f;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        leftTriggerValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis9");
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        leftTriggerValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis5");
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
        leftTriggerValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis3");
#endif
        return leftTriggerValue;
    }

    private float GetRightTriggerValue()
    {
        float rightTriggerValue = 0.0f;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        rightTriggerValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis10");
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        rightTriggerValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis6");
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
        rightTriggerValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis6");
#endif
        return rightTriggerValue;
    }

    private float GetDPadHorizontalValue()
    {
        float dpadHorizontalValue = 0.0f;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        dpadHorizontalValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis6");
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        switch (PlayerNumber)
        {
            case PlayerNumbers.Any:
                if (Input.GetKey(KeyCode.JoystickButton7))
                    dpadHorizontalValue = -1.0f;
                if (Input.GetKey(KeyCode.JoystickButton8))
                    dpadHorizontalValue = 1.0f;
                break;
            case PlayerNumbers.Player1:
                if (Input.GetKey(KeyCode.Joystick1Button7))
                    dpadHorizontalValue = -1.0f;
                if (Input.GetKey(KeyCode.Joystick1Button8))
                    dpadHorizontalValue = 1.0f;
                break;
            case PlayerNumbers.Player2:
                if (Input.GetKey(KeyCode.Joystick2Button7))
                    dpadHorizontalValue = -1.0f;
                if (Input.GetKey(KeyCode.Joystick2Button8))
                    dpadHorizontalValue = 1.0f;
                break;
            case PlayerNumbers.Player3:
                if (Input.GetKey(KeyCode.Joystick3Button7))
                    dpadHorizontalValue = -1.0f;
                if (Input.GetKey(KeyCode.Joystick3Button8))
                    dpadHorizontalValue = 1.0f;
                break;
            case PlayerNumbers.Player4:
                if (Input.GetKey(KeyCode.Joystick4Button7))
                    dpadHorizontalValue = -1.0f;
                if (Input.GetKey(KeyCode.Joystick4Button8))
                    dpadHorizontalValue = 1.0f;
                break;
        }
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
        dpadHorizontalValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis7");
#endif
        return dpadHorizontalValue;
    }

    private float GetDPadVerticalValue()
    {
        float dpadVerticalValue = 0.0f;
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        dpadVerticalValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis6");
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        switch (PlayerNumber)
        {
            case PlayerNumbers.Any:
                if (Input.GetKey(KeyCode.JoystickButton6))
                    dpadVerticalValue = -1.0f;
                if (Input.GetKey(KeyCode.JoystickButton5))
                    dpadVerticalValue = 1.0f;
                break;
            case PlayerNumbers.Player1:
                if (Input.GetKey(KeyCode.Joystick1Button6))
                    dpadVerticalValue = -1.0f;
                if (Input.GetKey(KeyCode.Joystick1Button5))
                    dpadVerticalValue = 1.0f;
                break;
            case PlayerNumbers.Player2:
                if (Input.GetKey(KeyCode.Joystick2Button6))
                    dpadVerticalValue = -1.0f;
                if (Input.GetKey(KeyCode.Joystick2Button5))
                    dpadVerticalValue = 1.0f;
                break;
            case PlayerNumbers.Player3:
                if (Input.GetKey(KeyCode.Joystick3Button6))
                    dpadVerticalValue = -1.0f;
                if (Input.GetKey(KeyCode.Joystick3Button5))
                    dpadVerticalValue = 1.0f;
                break;
            case PlayerNumbers.Player4:
                if (Input.GetKey(KeyCode.Joystick4Button6))
                    dpadVerticalValue = -1.0f;
                if (Input.GetKey(KeyCode.Joystick4Button5))
                    dpadVerticalValue = 1.0f;
                break;
        }
#endif
#if UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
        dpadVerticalValue = Input.GetAxis("Controller" + (int)_playerNumber + "Axis8");
#endif
        return dpadVerticalValue;
    }

}
