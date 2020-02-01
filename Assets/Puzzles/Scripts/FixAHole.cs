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

        puzzleBoard.Setter = PlayerInput.Instance.Player1;
    }

    public void StartPuzzle(PlayerController picker, PlayerController setter)
    {

    }

    private void PuzzleBoard_OnPuzzleCompleted()
    {
        OnCompleted();
        Debug.LogWarning("Completed!");
    }
}
