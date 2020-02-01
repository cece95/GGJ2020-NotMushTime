using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void SetPlayerController(PlayerController controller)
    {
        playerController = controller;
        playerMovement.SetPlayerController(controller);
    }

    public void SetAllowMovement(bool allow)
    {
        playerMovement.SetAllowMovement(allow);
    }
}
