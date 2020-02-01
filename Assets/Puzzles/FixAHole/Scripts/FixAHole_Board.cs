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

    // Start is called before the first frame update
    void Start()
    {
        InitializeBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void InsertPiece()
    {

    }
}
