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
		public Action onValidate;

		internal void Register()
		{
			this.jsInstance = RuntimeHandler.RegisterInstance(this);
		}

		protected virtual void OnValidate()
		{
			onValidate?.Invoke();
		}

		protected virtual void Awake()
		{
			Register();
			awake?.Invoke();
		}

		protected virtual void OnEnable()
		{
			onEnable?.Invoke();
		}

		protected virtual void OnDisable()
		{
			onDisable?.Invoke();
		}

		protected virtual void Start()
		{
			start?.Invoke();
		}

		protected virtual void OnDestroy()
		{
			onDestroy?.Invoke();
		}

		[ContextMenu(nameof(Rebuild))]
		private void Rebuild()
		{
			
		}
	}
}