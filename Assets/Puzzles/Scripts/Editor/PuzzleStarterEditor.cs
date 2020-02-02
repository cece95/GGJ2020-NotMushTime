using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PuzzleStarter))]
public class PuzzleStarterEditor : Editor
{
    private void OnSceneGUI()
	{
        PuzzleStarter puzzle = target as PuzzleStarter;
		Transform handleTransform = puzzle.transform;
        Quaternion handleRotation = Quaternion.identity;

		Vector3 portalLocation = puzzle.PortalPosition;
        
		EditorGUI.BeginChangeCheck();
		portalLocation = Handles.DoPositionHandle(portalLocation, handleRotation);
		if (EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(puzzle, "Move Point");
			EditorUtility.SetDirty(puzzle);
			puzzle.PortalPosition = portalLocation;
		}
	}
}
