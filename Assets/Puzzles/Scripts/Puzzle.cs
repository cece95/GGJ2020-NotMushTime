using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public delegate void PuzzleEnded(Puzzle puzzle);

    public event PuzzleEnded OnPuzzleCompleted;
    public event PuzzleEnded OnPuzzleQuit;
    public event PuzzleEnded OnPuzzleFailed;

    [SerializeField]
    private int playersRequired;

    public int PlayersRequired { get { return playersRequired; } }

    public Player[] Players { get; private set; }
    public PuzzleRenderer MyRenderer;

    public virtual void StartPuzzle(Player[] players)
    {
        Players = players;
    }

    public void SetRenderTexture(RenderTexture rt)
    {
        GetComponentInChildren<Camera>().targetTexture = rt;
    }

    protected void OnCompleted()
    {
        if(OnPuzzleCompleted != null)
        {
            OnPuzzleCompleted(this);
        }
    }

    protected void OnQuit()
    {
        if(OnPuzzleQuit != null)
        {
            OnPuzzleQuit(this);
        }
    }

    protected void OnFailed()
    {
        if(OnPuzzleFailed != null)
        {
            OnPuzzleFailed(this);
        }
    }
}
