using System;
using DefaultNamespace;
using Puerts;
using UnityEngine;

[ExecuteAlways]
public class MyUnityComponent : MonoBehaviour
{
	private JsEnv env;
	private void OnEnable()
	{
		Debug.Log("Enable");
		env = new JsEnv(new NeedleLoader(@"Packages/com.needle.puerts-ts-sample/Runtime/output"));
		env.ClearModuleCache();
		env.Eval("console.log(\"Test\")");
		env.Eval("require('MyComponent')");
		// env.Eval("MyComponent");
		// env.ExecuteModule(@"MyComponent");
		// env.Eval("console.log(\"Hello\");");
	}

	private void Update()
	{
		env.Tick();
	}
}