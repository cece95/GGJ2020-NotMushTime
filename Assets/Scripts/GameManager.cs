using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { if (instance == null) { instance = FindObjectOfType<GameManager>(); } if (instance == null) { GameObject go = new GameObject(); go.name = "Game Manager"; instance = go.AddComponent<GameManager>(); } return instance; } }
    private static GameManager instance;

    private Player[] players;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SetupPlayers();
        InitializeGame();
    }

    void InitializeGame()
    {
        // Play dialogue
    }

    void OnDialogueFinished()
    {
        // Start timer
        // Enable player movement
    }

    public void StartPuzzle(Player[] players, Puzzle puzzleToStart)
    {
        // Instantiate puzzle

        // Disable player movement
        foreach(Player player in players)
        {
            player.SetAllowMovement(false);
        }

        // Display puzzle
    }

    void OnPuzzleEnded(Player[] players)
    {
        // Enable player movement
        foreach (Player player in players)
        {
            player.SetAllowMovement(true);
        }

        // Fade out puzzle
    }

    void EnablePlayerMovement(int playerId, bool movementEnabled)
    {
        if(playerId < players.Length)
        {
            players[playerId].SetAllowMovement(movementEnabled);
        }
    }

    void SetupPlayers()
    {
        players = FindObjectsOfType<Player>();

        foreach(Player player in players)
        {
            if(player.name.Contains("Mush"))
            {
                player.SetPlayerController(PlayerInput.Instance.Player1);
            }
            else
            {
                player.SetPlayerController(PlayerInput.Instance.Player2);
            }
        }
    }
}
