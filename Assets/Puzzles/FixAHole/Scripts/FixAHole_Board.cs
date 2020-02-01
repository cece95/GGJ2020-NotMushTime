using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_Board : MonoBehaviour
{
    [SerializeField]
    private int BoardWidth;

    [SerializeField]
    private int BoardHeight;

    [SerializeField]
    private float BlockSize;

    public byte[,] Board = new byte[,] {{ 1, 1, 1, 1, 1, 1 },
                                        { 1, 1, 0, 0, 0, 1 },
                                        { 1, 0, 0, 0, 0, 1 },
                                        { 1, 0, 0, 1, 0, 1 },
                                        { 1, 1, 1, 1, 1, 1 }};

    public FixAHole_BoardPiece[,] PieceBoard;

    [SerializeField]
    private FixAHole_BoardPiece BoardPiecePrefab;

    private int selectionX;
    private int selectionY;

    private FixAHole_Piece selectedPiece;

    [SerializeField]
    private FixAHole_PickingArea pickingArea;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeBoard();

        pickingArea.OnPieceSelected += OnPieceSelected;
    }

    private void OnPieceSelected(FixAHole_Piece piece)
    {
        selectedPiece = piece;

        selectionX = 0;
        selectionY = 0;

        HighlightSelection();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectionX++;
            if(selectionX + selectedPiece.PieceWidth >= BoardWidth)
            {
                selectionX = BoardWidth - selectedPiece.PieceWidth;
            }
            HighlightSelection();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectionX--;
            if (selectionX < 0)
            {
                selectionX = 0;
            }
            HighlightSelection();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectionY--;
            if (selectionY < 0)
            {
                selectionY = 0;
            }
            HighlightSelection();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectionY++;
            if (selectionY + selectedPiece.PieceHeight >= BoardHeight)
            {
                selectionY = BoardHeight - selectedPiece.PieceHeight;
            }
            HighlightSelection();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            InsertPiece();
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(selectedPiece)
            {
                selectedPiece.RotatePiece(false);
                EnsurePieceInBounds();
                HighlightSelection();
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (selectedPiece)
            {
                selectedPiece.RotatePiece(true);
                EnsurePieceInBounds();
                HighlightSelection();
            }
        }
    }

    void EvaluateBoard()
    {

    }

    void InitializeBoard()
    {
        BoardWidth = Board.GetLength(1);
        BoardHeight = Board.GetLength(0);

        Vector3 Offset = new Vector3(BoardWidth - 1, -BoardHeight - 1, 0) * (-BlockSize / 2.0f);

        Debug.Log("Board width: " + BoardWidth + " Board Height: " + BoardHeight);

        PieceBoard = new FixAHole_BoardPiece[BoardWidth, BoardHeight];

        for( int x = 0; x < BoardWidth; ++x )
        {
            for(int y = 0; y < BoardHeight; ++y )
            {
                FixAHole_BoardPiece NewPiece = Instantiate(BoardPiecePrefab, transform.Find("BoardPieces"));

                NewPiece.transform.localPosition = new Vector3(x, -y, 0) * BlockSize + Offset;
                NewPiece.SetFilled(Board[y, x] == 1);

                PieceBoard[x, y] = NewPiece;
            }
        }
    }

    void HighlightSelection()
    {
        ResetHighlight();

        if (selectedPiece)
        {
            bool[,] pieceBlock = selectedPiece.GetStructure();

            int width = pieceBlock.GetLength(1);
            int height = pieceBlock.GetLength(0);

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    int currentX = selectionX + x;
                    int currentY = selectionY + y;

                    if (currentX < BoardWidth && currentY < BoardHeight)
                    {
                        PieceBoard[currentX, currentY].SetHighlight(pieceBlock[y, x]);
                    }
                }
            }
        }
    }

    void ResetHighlight()
    {
        for (int x = 0; x < BoardWidth; ++x)
        {
            for (int y = 0; y < BoardHeight; ++y)
            {
                PieceBoard[x, y].SetHighlight(false);
            }
        }
    }

    void InsertPiece()
    {
        if (selectedPiece)
        {
            bool[,] pieceBlock = selectedPiece.GetStructure();

            for (int x = 0; x < selectedPiece.PieceWidth; ++x)
            {
                for (int y = 0; y < selectedPiece.PieceHeight; ++y)
                {
                    int currentX = selectionX + x;
                    int currentY = selectionY + y;

                    if (currentX < BoardWidth && currentY < BoardHeight)
                    {
                        bool currentFill = PieceBoard[currentX, currentY].IsFilled;
                        PieceBoard[currentX, currentY].SetFilled(currentFill || pieceBlock[y, x]);
                    }
                }
            }

            selectedPiece = null;
            ResetHighlight();
        }
    }

    void EnsurePieceInBounds()
    {
        if(selectedPiece)
        {
            if(selectionX + selectedPiece.PieceWidth > BoardWidth)
            {
                selectionX--;
            }
            if(selectionY + selectedPiece.PieceHeight > BoardHeight)
            {
                selectionY--;
            }
        }
    }
}
