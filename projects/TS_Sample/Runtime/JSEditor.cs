using System;
using UnityEditor;

namespace Needle.Puerts
{
	public abstract class JSEditor : Editor
	{
		[JSFunction]
		public Action onEnable, onInspectorGUI;

		protected virtual void Awake()
		{
			RuntimeHandler.RegisterEditor(this);
		}

		private void OnEnable()
		{
			onEnable?.Invoke();
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			onInspectorGUI?.Invoke();
		}
	}
}