using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WorldGenerator))]
public class WorldGenerationEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector ();

		var worldGenerator = (WorldGenerator)target;

		if (GUILayout.Button ("Generate new World")) {
			worldGenerator.Generate();
		}
	}
}
