using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRenderer : MonoBehaviour
{
    private Animator animator;

    private MeshRenderer meshRenderer;

    private Puzzle puzzleToRender;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer)
        {
            meshRenderer.sortingOrder = 10;
        }
    }

    public void SetPuzzleToRender(Puzzle puzzle)
    {
        puzzleToRender = puzzle;

    }

    public void FadeOut()
    {
        if(animator)
        {
            animator.SetTrigger("Contract");
        }
    }
}
