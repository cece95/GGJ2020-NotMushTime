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

    [SerializeField]
    private float PerlinThreshold = 0.5f;

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

    private FixAHole_Piece selectedPiece;

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
        int xOffset = Random.Range(0, 1000);
        int yOffset = Random.Range(0, 1000);

        Board = new byte[height, width];
        for(int x = 0; x < width; ++x)
        {
            for(int y = 0; y < height; ++y)
            {
                byte value = 1;
                if( x > 0 && y > 0 && x < width - 1 && y < height - 1)
                {
                    if(Mathf.PerlinNoise((float)(x + xOffset) / width, (float)(y + yOffset) / height) > PerlinThreshold)
                    {
                        value = 0;
                    }
                }

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

        selectionX = 0;
        selectionY = 0;

        HighlightSelection();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedPiece)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectionX++;
                if (selectionX + selectedPiece.PieceWidth >= BoardWidth)
                {
                    selectionX = BoardWidth - selectedPiece.PieceWidth;
                }
                HighlightSelection();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectionX--;
                if (selectionX < 0)
                {
                    selectionX = 0;
                }
                HighlightSelection();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
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

            if (Input.GetKeyDown(KeyCode.L))
            {
                InsertPiece();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                selectedPiece.RotatePiece(false);
                EnsurePieceInBounds();
                HighlightSelection();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                selectedPiece.RotatePiece(true);
                EnsurePieceInBounds();
                HighlightSelection();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChuckCurrentPiece();
            }
        }
    }

    void EvaluateBoard()
    {

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

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    int currentX = selectionX + x;
                    int currentY = selectionY + y;

                    if (currentX < BoardWidth && currentY < BoardHeight)
                    {
                        PieceBoard[currentY, currentX].SetHighlight(pieceBlock[y, x]);
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
                        bool currentFill = PieceBoard[currentY, currentX].IsFilled;
                        PieceBoard[currentY, currentX].SetFilled(currentFill || pieceBlock[y, x]);
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
