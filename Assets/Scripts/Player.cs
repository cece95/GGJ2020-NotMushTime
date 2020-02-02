using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    public delegate void Interact(Player player);
    public event Interact OnInteract;

    private PlayerController playerController;
    private PlayerMovement playerMovement;

    private void Awake()
    {

    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    public void SetPlayerController(PlayerController controller)
    {
        EnsurePlayerMovement();

        playerController = controller;
        playerMovement.SetPlayerController(controller);
    }

    public void SetAllowMovement(bool allow)
    {
        playerMovement.SetAllowMovement(allow);
    }

    void EnsurePlayerMovement()
    {
        if(playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    private void FixedUpdate()
    {
        if(playerController != null)
        {
            if(playerController.IsGreenDown())
            {
                if(OnInteract != null)
                {
                    OnInteract(this);
                }
            }
        }
    }
}
