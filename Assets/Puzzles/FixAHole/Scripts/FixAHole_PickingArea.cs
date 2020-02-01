using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_PickingArea : MonoBehaviour
{
    public delegate void PieceDelegate(FixAHole_Piece piece);
    public event PieceDelegate OnPieceSelected;

    private int selectedPiece = 0;

    private FixAHole_Piece[] pieces;

    // Start is called before the first frame update
    void Awake()
    {
        pieces = GetComponentsInChildren<FixAHole_Piece>();
        SelectPiece(0);
    }

    public void SelectPiece(int pieceId)
    {
        pieceId = Mathf.Clamp(pieceId, 0, 4);
        selectedPiece = pieceId;

        for(int i = 0; i < 5; ++i)
        {
            pieces[i].SetHighlight(i == pieceId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SelectPiece(selectedPiece - 1);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            SelectPiece(selectedPiece + 1);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(OnPieceSelected != null)
            {
                OnPieceSelected.Invoke(pieces[selectedPiece]);
            }
        }
    }
}
