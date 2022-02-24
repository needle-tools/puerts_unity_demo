using System;
using DefaultNamespace;
using Puerts;
using UnityEngine;

[ExecuteAlways]
public class MyComponent : MonoBehaviour
{
	private void OnEnable()
	{
		var module = GetType().Name;
		RuntimeHandler.Reload(module);
		RuntimeHandler.Env.Eval($"require('{module}')");
	}
}