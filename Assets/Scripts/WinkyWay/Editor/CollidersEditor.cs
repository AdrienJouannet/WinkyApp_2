using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawColliders))]

public class CollidersEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawColliders draw = target as DrawColliders;
		
		if (GUILayout.Button("DRAW"))
		{
			draw.SetPoly();
		}
	}
}
