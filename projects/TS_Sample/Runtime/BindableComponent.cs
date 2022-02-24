using System;
using System.Runtime.CompilerServices;
using Puerts;
using UnityEngine;

[assembly: InternalsVisibleTo("com.tencent.puerts")]

namespace Needle.Puerts
{
	public class BindableComponent : MonoBehaviour
	{
		private string _moduleName = null;

		internal string moduleName
		{
			get
			{
				_moduleName ??= this.GetType().Name;
				return _moduleName;
			}
		}

		private JSObject jsInstance;

		public Action awake, onEnable, onDisable, start, earlyUpdate, update, lateUpdate, onDestroy;

		private void Awake()
		{
			this.jsInstance = RuntimeHandler.RegisterInstance(this);
			awake?.Invoke();
		}

		private void OnEnable()
		{
			onEnable?.Invoke();
		}

		private void OnDisable()
		{
			onDisable?.Invoke();
		}

		private void Start()
		{
			start?.Invoke();
		}

		private void OnDestroy()
		{
			onDestroy?.Invoke();
		}
	}
}