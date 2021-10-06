using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InspectorExample.Enums;

namespace InspectorExample.ScriptableObjects
{
	[CreateAssetMenu(fileName = "Data", menuName = "Inspector Example/Create Data Object", order = 0)]
	public class DataHolder : ScriptableObject
	{
		public string themeName = "Default";
		public List<Segment> segmentList;
		public List<Mesh> meshList;
	}
}
