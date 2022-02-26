using System;
using UnityEngine;
using Needle.Puerts;
using UnityEditor;

namespace PuertsTest
{
	[ExecuteInEditMode]
	public class Rotate : JSComponent
	{
		public float speed = 20;
		public bool randomColor = true; 
		public Color color;
		public Rotate other;

		private string test = "test myfsdfdsfsd";
	}
}