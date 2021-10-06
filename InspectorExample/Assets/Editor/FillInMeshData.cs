using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using InspectorExample.ScriptableObjects;
using InspectorExample.Components;

namespace InspectorExample.EditorScript {
	public class FillInMeshData : EditorWindow
	{
		//The current theme in the editor window, will reset when window is reopened
		DataHolder currentThemeData;
		//The skin ref that we will assign on enable
		GUISkin ourSkin;

		//The name of the new scriptable object that is spawned
		string newDataName = "";

		//Opens a window using a menu item
		[MenuItem("Inspector Example/Open Mesh Assigner")]
		static void OpenMenu()
		{
			//Create multiple instances of the editor window
			FillInMeshData window = CreateInstance<FillInMeshData>();
			window.titleContent.text = "Mesh Assignment";
			window.Show();
		}

		private void OnEnable()
		{
			//Using the resource folder, load our GUISkin
			ourSkin = (GUISkin)Resources.Load("Skins/OurGUISkin");
		}

		private void OnGUI()
		{
			//begin checking if any value between these change
			EditorGUI.BeginChangeCheck();
			currentThemeData = (DataHolder)EditorGUILayout.ObjectField(currentThemeData, typeof(DataHolder), true);
			//Stop checking, and then if something has changed, go through the loop
			if (EditorGUI.EndChangeCheck())
			{
				//Get everything that is selected and update the current theme and update the mesh
				foreach(GameObject GO in Selection.gameObjects)
				{
					if(GO.TryGetComponent(out WallPiece ourWallPiece))
					{
						ourWallPiece.currentTheme = currentThemeData;
						ourWallPiece.UpdateMesh();
					}
				}
			}
			//Set window GUI to our skin
			GUI.skin = ourSkin;

			//If there is no data selected, allow the user to generate new data
			if(currentThemeData == null)
			{
				//Text field, just so we can name the new scriptable object
				newDataName = EditorGUILayout.TextField("Asset name: ", newDataName);
				if(GUILayout.Button("Create New Theme"))
				{
					//Create the instance of the scriptable object
					DataHolder asset = ScriptableObject.CreateInstance<DataHolder>();
					//If the user has not given the object a name, set a default name
					if (newDataName == "") newDataName = "NewData";
					//Create the asset in the scriptable object folder (This can be any folder you like)
					AssetDatabase.CreateAsset(asset, "Assets/ScriptableObject/" + newDataName + ".asset");
					AssetDatabase.SaveAssets();

					//Open the window to display the new scriptable object
					EditorUtility.FocusProjectWindow();

					Selection.activeObject = asset;
					//Set the windows current theme to be this new scriptable object
					currentThemeData = asset;
				}
			}
		}
	}
}
