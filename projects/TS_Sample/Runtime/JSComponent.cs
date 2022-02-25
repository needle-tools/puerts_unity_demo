using System;
using System.Runtime.CompilerServices;
using Puerts;
using UnityEngine;

// [assembly: InternalsVisibleTo("com.tencent.puerts")]

namespace Needle.Puerts
{
	public interface IJSComponent
	{
		bool enabled { get; set; }
		bool isActiveAndEnabled { get; }
		Action awake { get; set; }
		Action onEnable { get; set; }
		Action onDisable { get; set; }
		Action start { get; set; }
		Action earlyUpdate { get; set; }
		Action update { get; set; }
		Action lateUpdate { get; set; }
		Action onDestroy { get; set; }
		Action onValidate { get; set; }
	}

	public abstract class JSComponent : MonoBehaviour, IJSComponent
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

		public new bool isActiveAndEnabled => base.isActiveAndEnabled;

		[JSFunction] public Action awake { get; set; }
		[JSFunction] public Action onEnable { get; set; }
		[JSFunction] public Action onDisable { get; set; }
		[JSFunction] public Action start { get; set; }
		[JSFunction] public Action earlyUpdate { get; set; }
		[JSFunction] public Action update { get; set; }
		[JSFunction] public Action lateUpdate { get; set; }
		[JSFunction] public Action onDestroy { get; set; }
		[JSFunction] public Action onValidate { get; set; }

		internal void Register()
		{
			this.jsInstance = RuntimeHandler.RegisterComponent(this);
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