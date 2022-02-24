using System;
using DefaultNamespace;
using Puerts;
using UnityEngine;
using UnityEngine.LowLevel;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Needle.Puerts
{
	[ExecuteInEditMode]
	public class RuntimeHandler : MonoBehaviour
	{
		private static JsEnv _env;

		public static JsEnv Env
		{
			get
			{
				EnsureInstance();
				_env ??= new JsEnv(new NeedleLoader(@"Packages/com.needle.puerts-ts-sample/Runtime/output"));
				return _env;
			}
		}

		private static RuntimeHandler _instance;

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

		public static void Reload(string name)
		{
			Env.ClearModuleCache();
		}

		public static JSObject CreateInstance(BindableComponent inst, string name)
		{
			Debug.Log("Create instance for " + name, inst as Object);
			var varName = $"{name}_{Time.frameCount}_{Random.Range(0, 100000)}";
			const string jsInst = "inst";
			const string csInst = "bind";

			PlayerLoopSystem.UpdateFunction cb = () => inst?.updateCallback?.Invoke();
			PlayerLoopHelper.AddUpdateCallback(inst, cb, PlayerLoopEvent.Update);

			var functionBindings = "";
			// if (update != null)
			// {
				functionBindings += $"if({jsInst}.update !== undefined) {csInst}.{nameof(BindableComponent.updateCallback)} = () => {{ {jsInst}.update(); }};\n";
			// }
			Debug.Log(functionBindings);

			var create = Env.Eval<Func<object, JSObject>>(
				$@"
const {varName} = require('{name}'); 
function create({csInst}){{
const {jsInst} = new {varName}.{name}({csInst});
{functionBindings}
return {jsInst};
}}; 
create
"
			);
			var jsInstance = create(inst);
			return jsInstance;
		}

		private void Update()
		{
			Env.Tick();
		}
	}
}