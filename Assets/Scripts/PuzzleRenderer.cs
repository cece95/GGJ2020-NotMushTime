using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRenderer : MonoBehaviour
{
    private Animator animator;

    private MeshRenderer meshRenderer;

    private Puzzle puzzleToRender;

    private RenderTexture puzzleRT;

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
        puzzleRT = new RenderTexture(512, 512, 16, RenderTextureFormat.ARGB32);
        if (puzzleRT.Create())
        {

            puzzleToRender = puzzle;
            puzzle.SetRenderTexture(puzzleRT);

            meshRenderer.material.SetTexture("_MainTex", puzzleRT);
        }
    }

    public void FadeOut()
    {
        if(animator)
        {
            animator.SetTrigger("Contract");
        }
    }
}
