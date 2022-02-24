using System;
using System.Collections.Generic;
using System.Reflection;
using Puerts;

namespace Needle.Puerts
{
	[Configure]
	public class AssemblyCSharpTypes
	{
		[Binding] private static IEnumerable<Type> Bindings => Assembly.Load("Assembly-CSharp").GetExportedTypes();
	}
}