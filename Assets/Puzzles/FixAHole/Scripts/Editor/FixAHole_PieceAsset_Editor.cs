using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FixAHole_PieceAsset))]
public class FixAHole_PieceAsset_Editor : Editor
{
    FixAHole_PieceAsset comp;
    bool showTileEditor = true;

    public void OnEnable()
    {
        comp = (FixAHole_PieceAsset)target;

        if (comp.PieceStructure == null)
        {
            comp.PieceStructure = new List<string>();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        return;

    }
}
