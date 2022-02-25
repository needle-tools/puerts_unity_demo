using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using DefaultNamespace;
using Puerts;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

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
				if (_env == null)
				{
					_loader.AddFolder(@"Packages/com.needle.puerts-ts-sample/Runtime/output");
					_env = new JsEnv(_loader);
				}
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

		private static void EnsureInstance()
		{
			if (_instance) return;
			_instance = FindObjectOfType<RuntimeHandler>();
			if (!_instance)
				_instance = new GameObject("Runtime Handler").AddComponent<RuntimeHandler>();
		}

		private static readonly NeedleLoader _loader = new NeedleLoader();
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

		public static void ReloadComponent(string name)
		{
			// TODO: we can reload single components
			ReloadAllComponents();
		}

		internal static int CurrentId;

		public static JSObject RegisterEditor(JSEditor inst)
		{
			var name = inst.GetType().Name;
			var reg = new RegisteredComponent(name, inst);
			// Instance.components.Add(reg);
			return reg.JsInstance;
		}

		public static JSObject RegisterComponent(IJSComponent inst)
		{
			var name = inst.GetType().Name;
			var reg = new RegisteredComponent(name, inst);
			Instance.components.Add(reg);
			return reg.JsInstance;
		}

		private static readonly Dictionary<Type, IList<MemberInfo>> functionsCache = new Dictionary<Type, IList<MemberInfo>>();

		public static string CreateEventBindings(object comp, string jsInst, string csInst, ref IList<MemberInfo> functions)
		{
			var functionBindings = "";

			if (functions == null)
			{
				var type = comp.GetType();
				if (!functionsCache.TryGetValue(type, out functions))
				{
					functions = type.GetMembers(BindingFlags.Instance | BindingFlags.Public)
						.Where(m => m.GetCustomAttribute(typeof(JSFunction)) != null)
						.ToArray();
					functionsCache.Add(type, functions);
				}
			}

			if (functions != null)
			{
				foreach (var field in functions)
				{
					AddBinding(field.Name);
				}
			}

			void AddBinding(string fn) => functionBindings += $"if({jsInst}.{fn} !== undefined) {csInst}.{fn} = () => {{ {jsInst}.{fn}(); }};\n";
			return functionBindings;
		}

		// private bool wasFocused = false;

		private void Update()
		{
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
				if (reg.Component is IJSComponent jsComponent)
				{
					if (!jsComponent.isActiveAndEnabled) continue;
					switch (evt)
					{
						case PlayerLoopEvent.EarlyUpdate:
							jsComponent.earlyUpdate?.Invoke();
							break;
						case PlayerLoopEvent.Update:
							jsComponent.update?.Invoke();
							break;
						case PlayerLoopEvent.PreLateUpdate:
						case PlayerLoopEvent.PostLateUpdate:
							jsComponent.lateUpdate?.Invoke();
							break;
					}
				}
			}
		}
	}

	public class RegisteredComponent
	{
		public bool Exists
		{
			get
			{
				if (Component is Object obj) return obj;
				return Component != null;
			}
		}

		public bool IsValid => Exists && JsInstance != null;
		public readonly string Name;
		public readonly object Component;
		public JSObject JsInstance { get; private set; }

		private IList<MemberInfo> functions;

		public RegisteredComponent(string name, object component)
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
			var varName = $"{name}_{RuntimeHandler.CurrentId++}";

			var eventBindings = RuntimeHandler.CreateEventBindings(this.Component, jsInst, csInst, ref functions);

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

			Debug.Log("Create instance for " + name + ":\n" + chunk, inst as Object);
			var create = env.Eval<Func<object, JSObject>>(chunk, varName);
			this.JsInstance = create(inst);

			if (isRecompile && this.JsInstance != null)
			{
				if (Component is IJSComponent jsComponent)
				{
					jsComponent.awake?.Invoke();
					if (jsComponent.enabled)
					{
						jsComponent.onEnable?.Invoke();
						jsComponent.start?.Invoke();
					}
				}
			}
		}
	}
}