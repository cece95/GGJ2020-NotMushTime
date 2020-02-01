using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_Piece : MonoBehaviour
{
    public int PieceHeight { get; private set; }
    public int PieceWidth { get; private set; }

    [SerializeField]
    public FixAHole_PieceAsset PieceAsset;

    private bool[,] PieceStructure;

    private SpriteRenderer spriteRenderer;
    private Animation animation;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animation>();

        Debug.Log(PieceAsset);
    }

    public void Initialize(FixAHole_PieceAsset pieceAsset)
    {
        PieceAsset = pieceAsset;
        GetPieceStructure();
        UpdateSprites();
    }

    void UpdateSprites()
    {
        //TODO: Actually put some code in
    }

    void GetPieceStructure()
    {
        int w = PieceAsset.PieceStructure[0].Length;
        int h = PieceAsset.PieceStructure.Count;

        PieceWidth = w;
        PieceHeight = h;

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
        int w = PieceHeight;
        int h = PieceWidth;

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

        PieceWidth = w;
        PieceHeight = h;

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

    public void Chuck()
    {
        animation.Play();
        ///TODO: Play animation
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
