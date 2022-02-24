using System;
using System.Collections.Generic;
using System.Reflection;
using DefaultNamespace;
using Puerts;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using Random = UnityEngine.Random;

namespace Needle.Puerts
{
	[ExecuteInEditMode]
	public class RuntimeHandler : MonoBehaviour
	{
		public static JsEnv Env
		{
			get
			{
				EnsureInstance();
				_env ??= new JsEnv(new NeedleLoader(@"Packages/com.needle.puerts-ts-sample/Runtime/output"));
				return _env;
			}
		}

		public static RuntimeHandler Instance
		{
			get
			{
				EnsureInstance();
				return _instance;
			}
		}

		public static void Reload(string name)
		{
			Env.ClearModuleCache();
		}


		private static void EnsureInstance()
		{
			if (_instance) return;
			_instance = FindObjectOfType<RuntimeHandler>();
			if (!_instance)
				_instance = new GameObject("Runtime Handler").AddComponent<RuntimeHandler>();
		}

		private static JsEnv _env;
		private static RuntimeHandler _instance;


		private readonly List<RegisteredComponent> components = new List<RegisteredComponent>();

		public static void ReloadAllComponents()
		{
			Debug.Log("RELOAD ALL components");
			Env.ClearModuleCache();
			foreach (var e in Instance.components)
			{
				e.Recreate();
			}
		}

		public static void ReloadComponents(string name)
		{
		}

		internal static int CurrentId;

		public static JSObject RegisterInstance(BindableComponent inst)
		{
			var name = inst.moduleName;
			var reg = new RegisteredComponent(name, inst);
			Instance.components.Add(reg);
			return reg.JsInstance;
		}

		public static string CreateEventBindings(string jsInst, string csInst)
		{
			var functionBindings = "";
			AddBinding(nameof(BindableComponent.awake));
			AddBinding(nameof(BindableComponent.onEnable));
			AddBinding(nameof(BindableComponent.onDisable));
			AddBinding(nameof(BindableComponent.start));
			AddBinding(nameof(BindableComponent.earlyUpdate));
			AddBinding(nameof(BindableComponent.update));
			AddBinding(nameof(BindableComponent.lateUpdate));
			AddBinding(nameof(BindableComponent.onDestroy));

			void AddBinding(string fn) => functionBindings += $"if({jsInst}.{fn} !== undefined) {csInst}.{fn} = () => {{ {jsInst}.{fn}(); }};\n";
			return functionBindings;
		}

		// private bool wasFocused = false;

		private void Update()
		{
			// var focused = UnityEditorInternal.InternalEditorUtility.isApplicationActive;
			// if (focused != wasFocused)
			// {
			// 	wasFocused = focused;
			// 	ReloadAllComponents();
			// }
			if (Time.frameCount % 10 == 0)
				AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

			Env.Tick();
		}

		private void Awake()
		{
			PlayerLoopHelper.AddUpdateCallback(this, this.OnEarlyUpdate, PlayerLoopEvent.EarlyUpdate);
			PlayerLoopHelper.AddUpdateCallback(this, this.OnUpdate, PlayerLoopEvent.Update);
			PlayerLoopHelper.AddUpdateCallback(this, this.OnLateUpdate, PlayerLoopEvent.PostLateUpdate);
		}

		private void OnEarlyUpdate() => InvokeEvents(PlayerLoopEvent.EarlyUpdate, components);
		private void OnUpdate() => InvokeEvents(PlayerLoopEvent.Update, components);
		private void OnLateUpdate() => InvokeEvents(PlayerLoopEvent.PostLateUpdate, components);

		private void InvokeEvents(PlayerLoopEvent evt, List<RegisteredComponent> list)
		{
			for (var index = 0; index < list.Count; index++)
			{
				var reg = list[index];
				if (!reg.Exists)
				{
					list.RemoveAt(index--);
					continue;
				}
				switch (evt)
				{
					case PlayerLoopEvent.EarlyUpdate:
						reg.Component.earlyUpdate?.Invoke();
						break;
					case PlayerLoopEvent.Update:
						reg.Component.update?.Invoke();
						break;
					case PlayerLoopEvent.PreLateUpdate:
					case PlayerLoopEvent.PostLateUpdate:
						reg.Component.lateUpdate?.Invoke();
						break;
				}
			}
		}
	}

	public class RegisteredComponent
	{
		public bool Exists => Component;
		public bool IsValid => Component && JsInstance != null;
		public readonly string Name;
		public readonly BindableComponent Component;
		public JSObject JsInstance { get; private set; }

		public RegisteredComponent(string name, BindableComponent component)
		{
			Name = name;
			Component = component;
			Recreate();
		}

		private const string jsInst = "inst";
		private const string csInst = "bind";

		public void Recreate()
		{
			var env = RuntimeHandler.Env;
			var isRecompile = JsInstance != null;

			var inst = Component;
			var name = Name;
			var varName = $"{name}_{inst.GetInstanceID()}_{RuntimeHandler.CurrentId++}";

			var eventBindings = RuntimeHandler.CreateEventBindings(jsInst, csInst);

			var chunk = $@"
const {varName} = require('{name}'); 
function create({csInst}){{
	const {jsInst} = new {varName}.{name}();
	{jsInst}.unity = {csInst};
	{eventBindings}
	return {jsInst};
}}; 
create
";

			Debug.Log("Create instance for " + name + ":\n" + chunk, inst);
			var create = env.Eval<Func<object, JSObject>>(chunk, varName);
			this.JsInstance = create(inst);

			if (isRecompile && this.JsInstance != null)
			{
				this.Component.awake?.Invoke();
				if (this.Component.enabled)
				{
					this.Component.onEnable?.Invoke();
					this.Component.start?.Invoke();
				}
			}
		}
	}
}