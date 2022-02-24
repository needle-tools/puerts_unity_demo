using System;
using Puerts;
using UnityEngine;

namespace Needle.Puerts
{
	public class BindableComponent : MonoBehaviour
	{
		private string moduleName = null;
		private JSObject jsInstance;

		public Action updateCallback;


		private void OnEnable()
		{
			if (string.IsNullOrWhiteSpace(moduleName))
				this.moduleName = this.GetType().Name;
			this.jsInstance = RuntimeHandler.CreateInstance(this, moduleName);
		}
	}
}