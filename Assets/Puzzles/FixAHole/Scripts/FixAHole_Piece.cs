using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_Piece : MonoBehaviour
{
    public int PieceHeight { get { return pieceHeight; } }
    public int PieceWidth { get { return pieceWidth; } }

    [SerializeField]
    public FixAHole_PieceAsset PieceAsset;

    private bool[,] PieceStructure;

    private SpriteRenderer spriteRenderer;

    private int pieceWidth;
    private int pieceHeight;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //PieceBlock = PieceAsset.PieceStructure;
        GetPieceStructure();

        Debug.Log(PieceAsset);
    }

    void GetPieceStructure()
    {
        int w = PieceAsset.PieceStructure[0].Length;
        int h = PieceAsset.PieceStructure.Count;

        pieceWidth = w;
        pieceHeight = h;

        bool[,] structure = new bool[h, w];

        for (int y = 0; y < h; ++y)
        {
            for (int x = 0; x < w; ++x)
            {
                structure[y, x] = PieceAsset.PieceStructure[y][x] == '1';
            }
        }

        PieceStructure = structure;
    }

    public void RotatePiece(bool rotateRight)
    {
        int w = pieceHeight;
        int h = pieceWidth;

        bool[,] structure = new bool[h, w];

        for (int y = 0; y < h; ++y)
        {
            for (int x = 0; x < w; ++x)
            {
                if (rotateRight)
                {
                    structure[y, x] = PieceStructure[w - x - 1, y];
                }
                else
                {
                    structure[y, x] = PieceStructure[x, h - y - 1];
                }
            }
        }

        pieceWidth = w;
        pieceHeight = h;

        PieceStructure = structure;
    }

    public bool[,] GetStructure()
    {
        return PieceStructure;
    }

    public void SetHighlight(bool isHighlighted)
    {
        if (spriteRenderer)
        {
            spriteRenderer.color = isHighlighted ? Color.white : Color.gray;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
