using System;
using System.Collections.Generic;
using System.IO;
using Puerts;
using UnityEditor;
using UnityEngine;

namespace Needle.Puerts.Loaders
{
	[CreateAssetMenu(menuName = "Typescript/Export Directory")]
	public class TypescriptDirectory : ScriptableObject, ILoader
	{
		[SerializeField, HideInInspector] public string scriptsDirectory; // data relative

		public bool debug = false;

		private TypescriptWatcher watcher;

		public void Init()
		{
			if (!Application.isEditor)
			{
				Debug.Log(scriptsDirectory);
				if (!string.IsNullOrWhiteSpace(scriptsDirectory))
				{
					var fullPath = Application.dataPath + "/" + scriptsDirectory;
					InitAt(fullPath);
				}
			}
#if UNITY_EDITOR
			else
			{
				InitEditor();
			}
#endif
		}

#if UNITY_EDITOR
		private void OnValidate() => InitEditor();
		private void Awake() => InitEditor();

		private void InitEditor()
		{
			var path = AssetDatabase.GetAssetPath(this);
			if (string.IsNullOrWhiteSpace(path))
			{
				Debug.LogError("Failed to find path for " + this, this);
				return;
			}
			var assetPath = Path.GetFullPath(path);
			var currentDirectory = Path.GetDirectoryName(assetPath);
			InitAt(currentDirectory);
		}
#endif
		private void InitAt(string directory)
		{
			if (Directory.Exists(directory)) 
			{
				if (debug)
					Debug.Log("Watching " + directory);
				this.watcher = new TypescriptWatcher(directory);
				files.Clear();
				this.RecursiveAddFiles(new DirectoryInfo(directory));
			}
			else Debug.LogWarning("Directory does not exist: " + directory);
		}

		private readonly Dictionary<string, string> files = new Dictionary<string, string>();

		private void RecursiveAddFiles(DirectoryInfo currentDir)
		{
			var allFiles = currentDir.GetFiles("*.ts", SearchOption.AllDirectories);
			if (debug)
				Debug.Log("Register " + allFiles.Length + " files");
			foreach (var file in allFiles)
			{
				var key = Path.ChangeExtension(file.Name, ".js");
				var path = Path.ChangeExtension(file.FullName, ".js");
				if (debug)
					Debug.Log("Register " + key + ": " + path);
				files.Add(key, path);
			}
			if (debug)
				Debug.Log("Did register " + files.Count + " files");
		}

		public bool FileExists(string filepath)
		{
			if (debug)
				Debug.Log("Has: " + filepath + ", known files: " + files.Count);
			var res = files.TryGetValue(filepath, out var path) && File.Exists(path);
			if (debug)
				Debug.Log(res);
			return res;
		}

		public string ReadFile(string filepath, out string debugPath)
		{
			if (debug)
				Debug.Log("Read: " + filepath);
			if (files.TryGetValue(filepath, out var fp))
			{
				if (File.Exists(fp))
				{
					debugPath = fp;
					return File.ReadAllText(fp);
				}
			}
			debugPath = null;
			return null;
		}
	}
}