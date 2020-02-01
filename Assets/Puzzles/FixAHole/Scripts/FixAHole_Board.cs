﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_Board : MonoBehaviour
{
    public delegate void PuzzleCompleted();
    public event PuzzleCompleted OnPuzzleCompleted;

    [SerializeField]
    private int BoardWidth;

    [SerializeField]
    private int BoardHeight;

    [SerializeField]
    private float BlockSize;

    [SerializeField]
    private float DigThreshold = 0.65f;

    [SerializeField]
    private int DesiredHoles = 20;

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

    private int currentHoles;

    private bool isCompleted = false;

    private float moveTimer;
    [SerializeField]
    private float TimeBetweenMoves = 0.3f;

    private FixAHole_Piece selectedPiece;

    public PlayerController Setter;
    public PlayerController Picker;

    [SerializeField]
    private FixAHole_PickingArea pickingArea;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeBoard();

        pickingArea.OnPieceSelected += OnPieceSelected;
    }

    void RandomizeBoard(int width, int height)
    {
        Board = new byte[height, width];
        for(int x = 0; x < width; ++x)
        {
            for(int y = 0; y < height; ++y)
            {
                Board[y, x] = 1;
            }
        }

        currentHoles = 0;

        while (currentHoles < DesiredHoles)
        {
            int holeSize = Random.Range(3, 5);
            DigHole(Random.Range(1, width - 1), Random.Range(1, height - 1), holeSize);
        }
    }

    void DigHole(int x, int y, int size)
    {
        if (x > 0 && y > 0 && x < BoardWidth - 1 && y < BoardHeight - 1 && currentHoles < DesiredHoles)
        {
            if (Board[y, x] == 1)
            {
                Board[y, x] = 0;
                ++currentHoles;
                --size;
            }

            if (size > 0 && Random.Range(0.0f, 100.0f) <= DigThreshold)
            {
                DigHole(x - 1, y, size--);
            }
            if (size > 0 && Random.Range(0.0f, 100.0f) <= DigThreshold)
            {
                DigHole(x + 1, y, size--);
            }
            if (size > 0 && Random.Range(0.0f, 100.0f) <= DigThreshold)
            {
                DigHole(x, y - 1, size--);
            }
            if (size > 0 && Random.Range(0.0f, 100.0f) <= DigThreshold)
            {
                DigHole(x, y + 1, size--);
            }
        }
    }

    int CalculateNeighbours(int x, int y)
    {
        if(Board[y, x] == 1)
        {
            return 0;
        }

        int counter = 0;

        if(x-1 >= 0 && Board[y, x-1] == 0)
        {
            counter |= 8;
        }
        if(x+1 < BoardWidth && Board[y, x+1] == 0)
        {
            counter |= 2;
        }
        if(y-1 >= 0 && Board[y-1, x] == 0)
        {
            counter |= 1;
        }
        if(y+1 < BoardHeight && Board[y+1, x] == 0)
        {
            counter |= 4;
        }

        return counter;
    }

    private void OnPieceSelected(FixAHole_Piece piece)
    {
        selectedPiece = piece;
        selectedPiece.transform.SetParent(null);
        selectedPiece.transform.position = new Vector3(selectedPiece.transform.position.x, selectedPiece.transform.position.y, -20.0f);

        EnsurePieceInBounds();

        HighlightSelection();
    }

    private void FixedUpdate()
    {
        if (selectedPiece)
        {
            selectedPiece.transform.position = Vector3.Lerp(selectedPiece.transform.position, PieceBoard[selectionY, selectionX].transform.position + new Vector3(0.0f, 0.0f, -2.0f), 0.5f);
        }

        if (moveTimer > 0.0f)
        {
            moveTimer -= Time.fixedDeltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        pickingArea.CanPickPiece = (selectedPiece == null && !isCompleted);

        if (selectedPiece && !isCompleted)
        {
            if(moveTimer <= 0.0f)
            {
                if(Setter.HorizontalPress > 0)
                {
                    selectionX++;
                    if (selectionX + selectedPiece.PieceWidth >= BoardWidth)
                    {
                        selectionX = BoardWidth - selectedPiece.PieceWidth;
                    }
                    HighlightSelection();
                    moveTimer = TimeBetweenMoves;
                }
                if (Setter.HorizontalPress < 0)
                {
                    selectionX--;
                    if (selectionX < 0)
                    {
                        selectionX = 0;
                    }
                    HighlightSelection();
                    moveTimer = TimeBetweenMoves;
                }
                if(Setter.VerticalPress > 0)
                {
                    selectionY--;
                    if (selectionY < 0)
                    {
                        selectionY = 0;
                    }
                    HighlightSelection();
                    moveTimer = TimeBetweenMoves;
                }
                if(Setter.VerticalPress < 0)
                {
                    selectionY++;
                    if (selectionY + selectedPiece.PieceHeight >= BoardHeight)
                    {
                        selectionY = BoardHeight - selectedPiece.PieceHeight;
                    }
                    HighlightSelection();
                    moveTimer = TimeBetweenMoves;
                }
            }
            else if( Setter.HorizontalPress == 0 && Setter.VerticalPress == 0 )
            {
                moveTimer = 0.0f;
            }

            if (Setter.IsBlueDown())
            {
                InsertPiece();
            }
            if (Setter.IsYellowDown())
            {
                selectedPiece.RotatePiece(false);
                EnsurePieceInBounds();
                HighlightSelection();
            }
            if (Setter.IsGreenDown())
            {
                selectedPiece.RotatePiece(true);
                EnsurePieceInBounds();
                HighlightSelection();
            }
            if (Setter.IsRedDown())
            {
                ChuckCurrentPiece();
            }
        }
    }

    void EvaluateBoard()
    {
        bool completed = true;
        for(int x = 0; x < BoardWidth; ++x)
        {
            for(int y = 0; y < BoardHeight; ++y)
            {
                if(Board[y,x] == 0)
                {
                    completed = false;
                    break;
                }
            }
        }

        isCompleted = completed;

        if(completed)
        {
            if(OnPuzzleCompleted != null)
            {
                OnPuzzleCompleted();
            }
        }
    }

    void InitializeBoard()
    {
        RandomizeBoard(BoardWidth, BoardHeight);

        BoardWidth = Board.GetLength(1);
        BoardHeight = Board.GetLength(0);

        Vector3 Offset = new Vector3(BoardWidth - 1, -BoardHeight - 1, 0) * (-BlockSize / 2.0f);

        Debug.Log("Board width: " + BoardWidth + " Board Height: " + BoardHeight);

        PieceBoard = new FixAHole_BoardPiece[BoardHeight, BoardWidth];

        for( int x = 0; x < BoardWidth; ++x )
        {
            for(int y = 0; y < BoardHeight; ++y )
            {
                FixAHole_BoardPiece NewPiece = Instantiate(BoardPiecePrefab, transform.Find("BoardPieces"));

                NewPiece.transform.localPosition = new Vector3(x, -y, 0) * BlockSize + Offset;
                NewPiece.SetFilled(Board[y, x] == 1);

                PieceBoard[y, x] = NewPiece;
            }
        }

        for (int x = 0; x < BoardWidth; ++x)
        {
            for (int y = 0; y < BoardHeight; ++y)
            {
                int neighbours = CalculateNeighbours(x, y);

                if (PieceBoard[y, x] != null)
                {
                    PieceBoard[y, x].SetNeighbours((byte)neighbours);
                }
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

            bool canPlace = CanPlacePiece();
            selectedPiece.SetBlocked(!canPlace);

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    int currentX = selectionX + x;
                    int currentY = selectionY + y;

                    if (currentX < BoardWidth && currentY < BoardHeight)
                    {
                        if (canPlace)
                        {
                            PieceBoard[currentY, currentX].SetHighlight(pieceBlock[y, x]);
                        }
                        else
                        {
                            PieceBoard[currentY, currentX].SetBlocked(pieceBlock[y, x]);
                        }
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
                PieceBoard[y, x].SetHighlight(false);
                PieceBoard[y, x].SetBlocked(false);
            }
        }
    }

    void InsertPiece()
    {
        if (selectedPiece && CanPlacePiece())
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
                        bool currentFill = PieceBoard[currentY, currentX].IsFilled;
                        bool isFilled = currentFill || pieceBlock[y, x];
                        Board[currentY, currentX] = (byte)(isFilled ? 1 : 0);
                        PieceBoard[currentY, currentX].SetFilled(isFilled);
                    }
                }
            }

            selectedPiece = null;
            ResetHighlight();
            EvaluateBoard();
        }
    }

    bool CanPlacePiece()
    {
        bool returnValue = false;

        if(selectedPiece)
        {
            returnValue = true;
            bool[,] pieceBlock = selectedPiece.GetStructure();

            for (int x = 0; x < selectedPiece.PieceWidth; ++x)
            {
                for (int y = 0; y < selectedPiece.PieceHeight; ++y)
                {
                    int currentX = selectionX + x;
                    int currentY = selectionY + y;

                    if (currentX < BoardWidth && currentY < BoardHeight)
                    {
                        bool currentFill = PieceBoard[currentY, currentX].IsFilled;
                        if(currentFill && pieceBlock[y,x])
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return returnValue;
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

    void ChuckCurrentPiece()
    {
        if(selectedPiece)
        {
            selectedPiece.Chuck();
            selectedPiece = null;
            HighlightSelection();
        }
    }
}