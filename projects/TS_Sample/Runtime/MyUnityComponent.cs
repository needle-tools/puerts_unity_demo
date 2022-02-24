using Puerts;
using UnityEngine;

[ExecuteAlways]
public class MyUnityComponent : MonoBehaviour
{
	private void OnEnable()
	{
		var env = new JsEnv();
		env.Eval("console.log(\"Hello\");");
	}
}