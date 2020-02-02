using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole : Puzzle
{
    private FixAHole_Board puzzleBoard;

    private void Awake()
    {
        puzzleBoard = GetComponentInChildren<FixAHole_Board>();
        puzzleBoard.OnPuzzleCompleted += PuzzleBoard_OnPuzzleCompleted;
        puzzleBoard.Initialize();

        puzzleBoard.SetPlayerControllers(PlayerInput.Instance.Player1, PlayerInput.Instance.Player2);
    }

    public override void StartPuzzle(Player[] players)
    {
        base.StartPuzzle(players);

        puzzleBoard = GetComponentInChildren<FixAHole_Board>();
        puzzleBoard.OnPuzzleCompleted += PuzzleBoard_OnPuzzleCompleted;
        puzzleBoard.Initialize();

        puzzleBoard.SetPlayerControllers(Players[0].GetPlayerController(), Players[1].GetPlayerController());
    }

    private void PuzzleBoard_OnPuzzleCompleted()
    {
        OnCompleted();
        Debug.LogWarning("Completed!");
    }
}
