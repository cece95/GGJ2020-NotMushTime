using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_Board : MonoBehaviour
{
    public delegate void PuzzleCompleted();
    public event PuzzleCompleted OnPuzzleCompleted;

    TriggerPuzzleSlide triggerPuzzleDiscard;
    TriggerPuzzleRotate triggerPuzzleRotate;

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

    private FixAHole_BoardPiece[,] pieceBoard;

    private FixAHole_BoardPiece boardPiecePrefab;

    private int selectionX;
    private int selectionY;

    private int currentHoles;

    private bool isCompleted = false;

    private float moveTimer;

    private FixAHole_Piece selectedPiece;

    private FixAHole_PickingArea pickingArea;

    [SerializeField]
    private PlayerController setter;

    private void Start()
    {
        triggerPuzzleRotate = GetComponent<TriggerPuzzleRotate>();
    }

    public void Initialize()
    {
        boardPiecePrefab = Resources.Load<FixAHole_BoardPiece>("FixAHole/Prefabs/Hole");
        pickingArea = transform.parent.gameObject.GetComponentInChildren<FixAHole_PickingArea>();

        InitializeBoard();

        pickingArea.OnPieceSelected += OnPieceSelected;
    }

    public void SetPlayerControllers(PlayerController setter, PlayerController picker)
    {
        this.setter = setter;
        pickingArea.Picker = picker;
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
        if(Board[y, x] == 0)
        {
            return 0;
        }

        int counter = 0;

        if(x-1 < 0 || Board[y, x-1] == 1)
        {
            counter |= 8;
        }
        if(x+1 >= BoardWidth || Board[y, x+1] == 1)
        {
            counter |= 2;
        }
        if(y-1 < 0 || Board[y-1, x] == 1)
        {
            counter |= 1;
        }
        if(y+1 >= BoardHeight || Board[y+1, x] == 1)
        {
            counter |= 4;
        }

        return counter;
    }

    private void OnPieceSelected(FixAHole_Piece piece)
    {
        selectedPiece = piece;
        selectedPiece.transform.SetParent(transform.parent);
        selectedPiece.transform.position = new Vector3(selectedPiece.transform.position.x, selectedPiece.transform.position.y, -20.0f);

        EnsurePieceInBounds();

        HighlightSelection();
    }

    private void FixedUpdate()
    {
        if (selectedPiece)
        {
            selectedPiece.transform.position = Vector3.Lerp(selectedPiece.transform.position, pieceBoard[selectionY, selectionX].transform.position + new Vector3(0.0f, 0.0f, -2.0f), 0.5f);
        }

        pickingArea.CanPickPiece = (selectedPiece == null && !isCompleted);

        if (selectedPiece && !isCompleted)
        {
            if (setter.HorizontalPress > 0)
            {
                selectionX++;
                if (selectionX + selectedPiece.PieceWidth >= BoardWidth)
                {
                    selectionX = BoardWidth - selectedPiece.PieceWidth;
                }

                if (triggerPuzzleRotate)
                    triggerPuzzleRotate.Trigger();
                HighlightSelection();
            }
            if (setter.HorizontalPress < 0)
            {
                selectionX--;
                if (selectionX < 0)
                {
                    selectionX = 0;
                }

                if (triggerPuzzleRotate)
                    triggerPuzzleRotate.Trigger();
                HighlightSelection();
            }
            if (setter.VerticalPress > 0)
            {
                selectionY--;
                if (selectionY < 0)
                {
                    selectionY = 0;
                }

                if (triggerPuzzleRotate)
                    triggerPuzzleRotate.Trigger();
                HighlightSelection();
            }
            if (setter.VerticalPress < 0)
            {
                selectionY++;
                if (selectionY + selectedPiece.PieceHeight >= BoardHeight)
                {
                    selectionY = BoardHeight - selectedPiece.PieceHeight;
                }

                if (triggerPuzzleRotate)
                    triggerPuzzleRotate.Trigger();
                HighlightSelection();
            }

            if (setter.IsBlueDown())
            {
                InsertPiece();
            }
            if (setter.IsYellowDown())
            {
                if (triggerPuzzleRotate)
                    triggerPuzzleRotate.Trigger();
                selectedPiece.RotatePiece(false);
                EnsurePieceInBounds();
                HighlightSelection();
            }
            if (setter.IsGreenDown())
            {
                if (triggerPuzzleRotate)
                    triggerPuzzleRotate.Trigger();
                selectedPiece.RotatePiece(true);
                EnsurePieceInBounds();
                HighlightSelection();
            }
            if (setter.IsRedDown())
            {
                if (triggerPuzzleDiscard)
                    triggerPuzzleDiscard.Trigger();
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

        pieceBoard = new FixAHole_BoardPiece[BoardHeight, BoardWidth];

        for( int x = 0; x < BoardWidth; ++x )
        {
            for(int y = 0; y < BoardHeight; ++y )
            {
                FixAHole_BoardPiece NewPiece = Instantiate(boardPiecePrefab, transform.Find("BoardPieces"));

                NewPiece.transform.localPosition = new Vector3(x, -y, 0) * BlockSize + Offset;
                NewPiece.SetFilled(Board[y, x] == 1);

                pieceBoard[y, x] = NewPiece;
            }
        }

        for (int x = 0; x < BoardWidth; ++x)
        {
            for (int y = 0; y < BoardHeight; ++y)
            {
                int neighbours = CalculateNeighbours(x, y);

                if (pieceBoard[y, x] != null)
                {
                    pieceBoard[y, x].SetNeighbours((byte)neighbours);
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
                            pieceBoard[currentY, currentX].SetHighlight(pieceBlock[y, x]);
                        }
                        else
                        {
                            pieceBoard[currentY, currentX].SetBlocked(pieceBlock[y, x]);
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
                pieceBoard[y, x].SetHighlight(false);
                pieceBoard[y, x].SetBlocked(false);
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
                        bool currentFill = pieceBoard[currentY, currentX].IsFilled;
                        bool isFilled = currentFill || pieceBlock[y, x];
                        Board[currentY, currentX] = (byte)(isFilled ? 1 : 0);
                        pieceBoard[currentY, currentX].SetFilled(isFilled);
                    }
                }
            }

            if (triggerPuzzleRotate)
                triggerPuzzleRotate.Trigger();

            selectedPiece.transform.localPosition += new Vector3(0.0f, 0.0f, 2.0f);
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
                        bool currentFill = pieceBoard[currentY, currentX].IsFilled;
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
