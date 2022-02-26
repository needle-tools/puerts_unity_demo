using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Needle.Puerts.Loaders;
using Puerts;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Needle.Puerts
{
	[Configure]
	public class TypesInScriptsDirectories
	{
		[Binding] private static IEnumerable<Type> Bindings
		{
			get
			{
				var types = new List<Type>();
				var typeDirectories = AssetDatabase.FindAssets("t:" + nameof(TypescriptDirectory));
				var asmdefFiles = AssetDatabase.FindAssets("t:" + nameof(AssemblyDefinitionAsset))
					.Select(AssetDatabase.GUIDToAssetPath)
					.ToArray();
				var asmdefDirectories = asmdefFiles.Select(Path.GetDirectoryName).ToArray();
				if (asmdefDirectories.Length <= 0) return types;
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();
				foreach (var guid in typeDirectories)
				{
					var path = AssetDatabase.GUIDToAssetPath(guid);
					var dir = Path.GetDirectoryName(path);
					if (string.IsNullOrEmpty(dir)) continue;
					for (var index = 0; index < asmdefDirectories.Length; index++)
					{
						var asmdefDir = asmdefDirectories[index];
						if (dir.StartsWith(asmdefDir))
						{
							var asset = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(asmdefFiles[index]);
							var asmdef = JsonUtility.FromJson(asset.text, typeof(AssemblyDefinition)) as AssemblyDefinition;
							if (asmdef != null)
							{
								foreach (var assembly in assemblies)
								{
									if (assembly.GetName().Name == asmdef.name)
									{
										types.AddRange(assembly.GetExportedTypes());
									}
								}
							}
							break;
						}
					}
				}
				return types;
			}
		}

		[Serializable]
		private class AssemblyDefinition
		{
			public string name;
		}
	}
}