using System;
using System.Collections.Generic;
using System.Reflection;
using DefaultNamespace;
using Puerts;
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


		private const string jsInst = "inst";
		private const string csInst = "bind";
		private List<RegisteredComponent> components = new List<RegisteredComponent>();

		public static JSObject RegisterInstance(BindableComponent inst)
		{
			var name = inst.moduleName;
			Debug.Log("Create instance for " + name, inst);
			var varName = $"{name}_{Time.frameCount}_{Random.Range(0, 100000)}";

			var eventBindings = CreateEventBindings();
			Debug.Log(eventBindings);

			var create = Env.Eval<Func<object, JSObject>>(
				$@"
const {varName} = require('{name}'); 
function create({csInst}){{
const {jsInst} = new {varName}.{name}({csInst});
{eventBindings}
return {jsInst};
}}; 
create
"
			);
			var jsInstance = create(inst);

			Instance.components.Add(new RegisteredComponent() { Component = inst, JsInstance = jsInstance });

			return jsInstance;
		}

		private static string CreateEventBindings()
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
		public BindableComponent Component;
		public JSObject JsInstance;
	}
}