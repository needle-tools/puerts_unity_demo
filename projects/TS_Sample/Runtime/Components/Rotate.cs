using System;
using UnityEngine;
using Needle.Puerts;

namespace PuertsTest
{
	[ExecuteInEditMode]
	public class Rotate : JSComponent
	{
		public float speed = 20;
		public bool randomColor = true;
		public Color color;
	}
}