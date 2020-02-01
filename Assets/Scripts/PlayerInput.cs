using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public float Horizontal;
    public float Vertical;

    public bool Yellow;
    public bool Green;
    public bool Blue;
    public bool Red;
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

    void Update()
    {
        Player1.Horizontal = Input.GetAxis("P1 Horizontal");
        Player1.Vertical = Input.GetAxis("P1 Vertical");
        Player1.Yellow = Input.GetButton("P1 Yellow");
        Player1.Green = Input.GetButton("P1 Green");
        Player1.Blue = Input.GetButton("P1 Blue");
        Player1.Red = Input.GetButton("P1 Red");

        Player2.Horizontal = Input.GetAxis("P2 Horizontal");
        Player2.Vertical = Input.GetAxis("P2 Vertical");
        Player2.Yellow = Input.GetButton("P2 Yellow");
        Player2.Green = Input.GetButton("P2 Green");
        Player2.Blue = Input.GetButton("P2 Blue");
        Player2.Red = Input.GetButton("P2 Red");


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

