using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using InspectorExample.Components;

namespace InspectorExample.EditorScript
{
	[CustomEditor(typeof(WallPiece)), CanEditMultipleObjects]
	public class CustomInspectorExample : Editor
	{
		//This object
		WallPiece thisTarget;
		//Serialised Properties
		SerializedProperty segmentType;
		SerializedProperty theme;


		private void OnEnable()
		{
			thisTarget = (WallPiece)target;
			segmentType = serializedObject.FindProperty("currentSegment");
			theme = serializedObject.FindProperty("currentTheme");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			//Check if these values have changed, and if they have, update the mesh
			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(segmentType);
			EditorGUILayout.PropertyField(theme);
			serializedObject.ApplyModifiedProperties();
			if (EditorGUI.EndChangeCheck())
			{

				thisTarget.UpdateMesh();
			}
		}
	}
}

