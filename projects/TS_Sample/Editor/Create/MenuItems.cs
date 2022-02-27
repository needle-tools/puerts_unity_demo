using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Needle.Puerts
{
	public static class MenuItems
	{
		[MenuItem("Assets/Create/Typescript/Script", priority = 80)]
		public static void CreateTypescriptScript()
		{
			var sel = Selection.assetGUIDs.FirstOrDefault();
			if (!string.IsNullOrWhiteSpace(sel))
			{
				var templateGuid = "9bf30413f5374536badf6be0a62bc021";
				var templatePath = AssetDatabase.GUIDToAssetPath(templateGuid);
				if (string.IsNullOrWhiteSpace(templatePath) || !File.Exists(templatePath))
				{
					Debug.LogError("Could not find template: " + templateGuid + "; " + templatePath);
					return;
				}
				var templateContent = File.ReadAllText(templatePath);
				var fileName = "NewTypscriptComponent";
				templateContent = templateContent.Replace("$ClassName", fileName);
				var path = AssetDatabase.GUIDToAssetPath(sel);
				if (File.Exists(path)) path = Path.GetDirectoryName(path);
				if (string.IsNullOrWhiteSpace(path)) return;
				File.WriteAllText($"{path}/{fileName}.ts", templateContent);
				AssetDatabase.ImportAsset(path);
				AssetDatabase.Refresh();
				Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(path);
			}
		}

		[MenuItem("Assets/Create/Typescript/Config", priority = 80)]
		public static void CreateTsConfig()
		{
			var sel = Selection.assetGUIDs.FirstOrDefault();
			if (!string.IsNullOrWhiteSpace(sel))
			{
				var path = AssetDatabase.GUIDToAssetPath(sel);
				CreateTsConfig(path);
			}
		}

		public static string CreateTsConfig(string path, string outDir = null)
		{
			var templateGuid = "1e1d43992ed743b8a195982130bea04c";
			var templatePath = AssetDatabase.GUIDToAssetPath(templateGuid);
			if (string.IsNullOrWhiteSpace(templatePath) || !File.Exists(templatePath))
			{
				Debug.LogError("Could not find template: " + templateGuid + "; " + templatePath);
				return null;
			}
			var templateContent = File.ReadAllText(templatePath);
			if (outDir != null)
			{
				// https://regex101.com/r/EbwZo4/2
				templateContent = Regex.Replace(templateContent, "(\"outDir\":\\s*?\")(?<path>.*)(\")", "$1" + outDir + "\"");
			}
			var fileName = "tsconfig";
			if (File.Exists(path)) path = Path.GetDirectoryName(path);
			if (string.IsNullOrWhiteSpace(path)) return null;
			File.WriteAllText($"{path}/{fileName}.json", templateContent);
			AssetDatabase.ImportAsset(path);
			AssetDatabase.Refresh();
			Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(path);
			return path;
		}
	}
}