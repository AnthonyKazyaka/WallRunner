using UnityEngine;
using System.Collections;

public class Controller 
{

    public enum PlayerNumbers
    {
        Any = 0,
        Player1 = 1,
        Player2 = 2,
        Player3 = 3,
        Player4 = 4
    }

    public PlayerNumbers PlayerNumber { get; set; }

    public Controller()
    {
        PlayerNumber = PlayerNumbers.Any;
    }

    public Controller(PlayerNumbers playerNumber)
    {
        PlayerNumber = playerNumber;
    }

}
