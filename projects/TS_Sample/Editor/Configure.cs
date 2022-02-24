using System;
using System.Collections.Generic;
using Puerts;
using UnityEngine;

namespace Needle.Puerts
{
	[Configure]
	public class ExamplesCfg
	{
		[Binding]
		static IEnumerable<Type> Bindings
		{
			get
			{
				return new List<Type>()
				{
					typeof(Time),
					typeof(GameObject),
					typeof(Component),
					typeof(MonoBehaviour),
					typeof(Transform),
					typeof(Vector3),
				};
			}
		}
	}
}