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

        StartPuzzle(PlayerInput.Instance.Player1, PlayerInput.Instance.Player2);
    }

    public void StartPuzzle(PlayerController picker, PlayerController setter)
    {
        puzzleBoard.SetPlayerControllers(setter, picker);
    }

    private void PuzzleBoard_OnPuzzleCompleted()
    {
        OnCompleted();
        Debug.LogWarning("Completed!");
    }
}
