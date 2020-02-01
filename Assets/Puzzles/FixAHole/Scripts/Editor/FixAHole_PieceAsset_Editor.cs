using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FixAHole_PieceAsset))]
public class FixAHole_PieceAsset_Editor : Editor
{
    FixAHole_PieceAsset comp;
    bool showTileEditor = false;

    public void OnEnable()
    {
        comp = (FixAHole_PieceAsset)target;
        if (comp.PieceStructure == null)
        {
            comp.PieceStructure = new bool[comp.PieceWidth, comp.PieceHeight];
        }
    }

    public override void OnInspectorGUI()
    {
        comp.PieceName = EditorGUILayout.TextField("Name", comp.PieceName);
        comp.SpriteAsset = (Sprite)EditorGUILayout.ObjectField(comp.SpriteAsset, typeof(Sprite), false, GUILayout.Width(65f), GUILayout.Height(65f));

        EditorGUI.BeginChangeCheck();
        comp.PieceWidth = EditorGUILayout.IntField(comp.PieceWidth);
        comp.PieceHeight = EditorGUILayout.IntField(comp.PieceHeight);
        if (EditorGUI.EndChangeCheck())
        {
            int oldWidth = comp.PieceStructure.GetLength(1);
            int oldHeight = comp.PieceStructure.GetLength(0);

            bool[,] newPieceStructure = new bool[comp.PieceHeight, comp.PieceWidth];
            for(int h = 0; h < Mathf.Min(comp.PieceHeight, oldHeight); ++h )
            {
                for (int w = 0; w < Mathf.Min(comp.PieceWidth, oldWidth); ++w)
                {
                    newPieceStructure[w, h] = comp.PieceStructure[w, h];
                }
            }
        }
         
        showTileEditor = EditorGUILayout.Foldout(showTileEditor, "Tile Editor");

        if (showTileEditor)
        {
            for (int h = 0; h < comp.PieceHeight; h++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int w = 0; w < comp.PieceWidth; w++)
                {
                    comp.PieceStructure[w, h] = EditorGUILayout.Toggle(comp.PieceStructure[w, h]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
