using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InspectorExample.Enums;
using InspectorExample.ScriptableObjects;


namespace InspectorExample.Enums
{
	public enum Segment
	{
		Wall,
		Window,
		Door,
		Pillar,
		WideWindow
	}
}

namespace InspectorExample.Components
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class WallPiece : MonoBehaviour
    {
		[HideInInspector]
		public Segment currentSegment;
		[HideInInspector]
		public DataHolder currentTheme;
		MeshFilter mesh => this.transform.GetComponent<MeshFilter>();
		MeshCollider col => this.transform.GetComponent<MeshCollider>();


		public void UpdateMesh()
        {
			if(currentTheme != null)
			{
				if (currentTheme.segmentList.Count == currentTheme.meshList.Count)
				{
					int indexToGet = currentTheme.segmentList.IndexOf(currentSegment);
					if (indexToGet < currentTheme.meshList.Count && indexToGet != -1) {
						mesh.mesh = currentTheme.meshList[indexToGet];
					}
				}
			}
        }

	}
}
