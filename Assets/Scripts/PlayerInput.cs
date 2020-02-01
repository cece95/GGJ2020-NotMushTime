using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public float Horizontal;
    public float Vertical;

    public int HorizontalPress;
    public int VerticalPress;

    public bool Yellow;
    public bool Green;
    public bool Blue;
    public bool Red;

    public bool YellowPressedFlag;
    public bool GreenPressedFlag;
    public bool BluePressedFlag;
    public bool RedPressedFlag;

    /// <summary>
    /// Returns true if key was pressed during this frame
    /// </summary>
    /// <returns></returns>
    public bool IsYellowDown()
    {
        bool toRet = Yellow && !YellowPressedFlag;
        YellowPressedFlag = Yellow;
        return toRet;
    }

    /// <summary>
    /// Returns true if key was pressed during this frame
    /// </summary>
    /// <returns></returns>
    public bool IsGreenDown()
    {
        bool toRet = Green && !GreenPressedFlag;
        GreenPressedFlag = Green;
        return toRet;
    }

    /// <summary>
    /// Returns true if key was pressed during this frame
    /// </summary>
    /// <returns></returns>
    public bool IsBlueDown()
    {
        bool toRet = Blue && !BluePressedFlag;
        BluePressedFlag = Blue;
        return toRet;
    }

    /// <summary>
    /// Returns true if key was pressed during this frame
    /// </summary>
    /// <returns></returns>
    public bool IsRedDown()
    {
        bool toRet = Red && !RedPressedFlag;
        RedPressedFlag = Red;
        return toRet;
    }
}

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get { if (instance == null) instance = FindObjectOfType<PlayerInput>(); return instance; } }
    private static PlayerInput instance;

    //player 1 controls
    public float P1Vertical;
    public float P1Horizontal;
    public bool P1Red;
    public bool P1Green;
    [HideInInspector] public bool P1Yellow;
    [HideInInspector] public bool P1Blue;

    //player 2 controls
    [HideInInspector] public float P2Vertical;
    [HideInInspector] public float P2Horizontal;
    [HideInInspector] public bool P2Red;
    [HideInInspector] public bool P2Green;
    [HideInInspector] public bool P2Yellow;
    [HideInInspector] public bool P2Blue;

    public PlayerController Player1 = new PlayerController();
    public PlayerController Player2 = new PlayerController();

    void Awake()
    {

    }

    void FixedUpdate()
    {
        // Get all inputs
        Player1.HorizontalPress = (int)Input.GetAxis("P1 Horizontal Press");
        Player1.VerticalPress = (int)Input.GetAxis("P1 Vertical Press");

        Player1.Horizontal = Input.GetAxis("P1 Horizontal");
        Player1.Vertical = Input.GetAxis("P1 Vertical");
        Player1.Yellow = Input.GetButton("P1 Yellow");
        Player1.Green = Input.GetButton("P1 Green");
        Player1.Blue = Input.GetButton("P1 Blue");
        Player1.Red = Input.GetButton("P1 Red");

        // Reset pressed flags
        if (Player1.YellowPressedFlag && !Player1.Yellow)
        {
            Player1.YellowPressedFlag = false;
        }
        if (Player1.GreenPressedFlag && !Player1.Green)
        {
            Player1.GreenPressedFlag = false;
        }
        if (Player1.BluePressedFlag && !Player1.Blue)
        {
            Player1.BluePressedFlag = false;
        }
        if (Player1.RedPressedFlag && !Player1.Red)
        {
            Player1.RedPressedFlag = false;
        }

        // Get all inputs
        Player2.HorizontalPress = (int)Input.GetAxis("P2 Horizontal Press");
        Player2.VerticalPress = (int)Input.GetAxis("P2 Vertical Press");

        Player2.Horizontal = Input.GetAxis("P2 Horizontal");
        Player2.Vertical = Input.GetAxis("P2 Vertical");
        Player2.Yellow = Input.GetButton("P2 Yellow");
        Player2.Green = Input.GetButton("P2 Green");
        Player2.Blue = Input.GetButton("P2 Blue");
        Player2.Red = Input.GetButton("P2 Red");

        // Reset pressed flags
        if (Player2.YellowPressedFlag && !Player2.Yellow)
        {
            Player2.YellowPressedFlag = false;
        }
        if (Player2.GreenPressedFlag && !Player2.Green)
        {
            Player2.GreenPressedFlag = false;
        }
        if (Player2.BluePressedFlag && !Player2.Blue)
        {
            Player2.BluePressedFlag = false;
        }
        if (Player2.RedPressedFlag && !Player2.Red)
        {
            Player2.RedPressedFlag = false;
        }


        // TODO: Deprecated stuff - to remove
        P1Horizontal = Input.GetAxis("P1 Horizontal");
        P1Vertical = Input.GetAxis("P1 Vertical");
        P1Red = Input.GetButton("P1 Red");
        P1Green = Input.GetButton("P1 Green");
        P1Yellow = Input.GetButton("P1 Yellow");
        P1Blue = Input.GetButton("P1 Blue");

        P2Horizontal = Input.GetAxis("P2 Horizontal");
        P2Vertical = Input.GetAxis("P2 Vertical");
        P2Red = Input.GetButton("P2 Red");
        P2Green = Input.GetButton("P2 Green");
        P2Yellow = Input.GetButton("P2 Yellow");
        P2Blue = Input.GetButton("P2 Blue");
    }
}

