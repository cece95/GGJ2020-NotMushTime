using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public delegate void PuzzleCompleted();
    public delegate void PuzzleFailed();

    public event PuzzleCompleted OnPuzzleCompleted;
    public event PuzzleFailed OnPuzzleFailed;

    protected void OnCompleted()
    {
        if(OnPuzzleCompleted != null)
        {
            OnPuzzleCompleted();
        }
    }

    protected void OnFailed()
    {
        if(OnPuzzleFailed != null)
        {
            OnPuzzleFailed();
        }
    }
}
