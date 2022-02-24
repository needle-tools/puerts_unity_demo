using Puerts;
using UnityEngine;

namespace DefaultNamespace
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

		private void Update()
		{
			Env.Tick();
		}
	}
}