#if UNITY_EDITOR
using System;
using PuertsTest;
using UnityEditor;

namespace Needle.Puerts
{
	[CustomEditor(typeof(Rotate))]
	public class RotateEditor : JSEditor
	{
	}
}
#endif