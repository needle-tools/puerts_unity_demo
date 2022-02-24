using System;
using System.Collections.Generic;
using Puerts;

namespace Needle.Puerts
{
	[Configure]
	internal class TypesInThisAssembly
	{
		[Binding]
		private static IEnumerable<Type> Bindings
		{
			get
			{
				var type = typeof(TypesInThisAssembly);
				var assembly = type.Assembly;
				var list = new List<Type>();
				foreach (var t in assembly.GetExportedTypes())
				{
					list.Add(t);
				}
				return list;
			}
		}
	}
}