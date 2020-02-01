using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FixAHole Piece", order = 1)]
public class FixAHole_PieceAsset : ScriptableObject
{
    [SerializeField]
    public int PieceWidth;

    [SerializeField]
    public int PieceHeight;

    [SerializeField]
    public string PieceName;

    [SerializeField]
    public List<string> PieceStructure;

    [SerializeField]
    public Sprite SpriteAsset;
}
