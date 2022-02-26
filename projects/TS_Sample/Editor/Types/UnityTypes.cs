using System;
using System.Collections.Generic;
using Puerts;
using UnityEngine;

namespace Needle.Puerts
{
	[Configure]
	public class UnityTypes
	{
		// This tag is only called for ts. Compared with Binding, this tag only generates ts declarations (that is, no static classes are generated, and only function declarations are generated in index.d.ts for ts to call).
		[Typing]
		static IEnumerable<Type> Bindings
		{
			get
			{
				var list = new List<Type>();
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();
				foreach (var asm in assemblies)
				{
					if (asm.FullName.Contains("UnityEngine") || asm.FullName.Contains("Unity"))
					{
						foreach (var type in asm.GetExportedTypes())
						{
							list.Add(type);
						}
					}
				}
				return list;
			}
		}
	}
}