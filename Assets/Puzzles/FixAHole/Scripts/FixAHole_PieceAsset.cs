using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FixAHole Piece", order = 1)]
public class FixAHole_PieceAsset : ScriptableObject
{
    public int PieceWidth;
    public int PieceHeight;
    public string PieceName;
    public bool[,] PieceStructure;
    public Sprite SpriteAsset;
}
