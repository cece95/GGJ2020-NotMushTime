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

    public float StickPressDowntime;

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
    public static PlayerInput Instance {
                            get {
                                instance = FindObjectOfType<PlayerInput>();
                                if (instance == null)
                                {
                                    GameObject go = new GameObject();
                                    go.name = "Player Input";
                                    instance = go.AddComponent<PlayerInput>();
                                }
                                return instance;
                            } }
    private static PlayerInput instance;

    [SerializeField]
    private float stickPressDowntime = 0.2f;

    [SerializeField]
    private float stickReleaseThreshold = 0.15f;

    public float StickPressDowntime { get { return stickPressDowntime; } }

    //player 1 controls
    [HideInInspector] public float P1Vertical;
    [HideInInspector] public float P1Horizontal;
    [HideInInspector] public bool P1Red;
    [HideInInspector] public bool P1Green;
    [HideInInspector] public bool P1Yellow;
    [HideInInspector] public bool P1Blue;

    //player 2 controls
    [HideInInspector] public float P2Vertical;
    [HideInInspector] public float P2Horizontal;
    [HideInInspector] public bool P2Red;
    [HideInInspector] public bool P2Green;
    [HideInInspector] public bool P2Yellow;
    [HideInInspector] public bool P2Blue;

    public PlayerController Player1 { get { return player1Controller; } }
    public PlayerController Player2 { get { return player2Controller; } }

    private PlayerController player1Controller;
    private PlayerController player2Controller;

    void Awake()
    {
        player1Controller = new PlayerController();
        player2Controller = new PlayerController();
    }

    void UpdatePlayerInput(ref PlayerController controller, string playerId)
    {
        if(controller == null)
        {
            return;
        }

        // Get all inputs
        controller.StickPressDowntime -= Time.fixedDeltaTime;
        if (controller.StickPressDowntime <= 0.0f)
        {
            controller.HorizontalPress = (int)Input.GetAxis(playerId + " Horizontal Press");
            controller.VerticalPress = (int)Input.GetAxis(playerId + " Vertical Press");

            if (controller.HorizontalPress != 0.0f || controller.VerticalPress != 0.0f)
            {
                controller.StickPressDowntime = stickPressDowntime;
            }
        }
        else
        {
            if (Mathf.Abs(Input.GetAxis(playerId + " Horizontal Press")) <= stickReleaseThreshold && Mathf.Abs(Input.GetAxis(playerId + " Vertical Press")) <= stickReleaseThreshold)
            {
                controller.StickPressDowntime = 0.0f;
            }

            controller.HorizontalPress = 0;
            controller.VerticalPress = 0;
        }

        controller.Horizontal = Input.GetAxis(playerId + " Horizontal");
        controller.Vertical = Input.GetAxis(playerId + " Vertical");
        controller.Yellow = Input.GetButton(playerId + " Yellow");
        controller.Green = Input.GetButton(playerId + " Green");
        controller.Blue = Input.GetButton(playerId + " Blue");
        controller.Red = Input.GetButton(playerId + " Red");

        // Reset pressed flags
        if (controller.YellowPressedFlag && !controller.Yellow)
        {
            controller.YellowPressedFlag = false;
        }
        if (controller.GreenPressedFlag && !controller.Green)
        {
            controller.GreenPressedFlag = false;
        }
        if (controller.BluePressedFlag && !controller.Blue)
        {
            controller.BluePressedFlag = false;
        }
        if (controller.RedPressedFlag && !controller.Red)
        {
            controller.RedPressedFlag = false;
        }
    }

    void FixedUpdate()
    {
        UpdatePlayerInput(ref player1Controller, "P1");
        UpdatePlayerInput(ref player2Controller, "P2");

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

