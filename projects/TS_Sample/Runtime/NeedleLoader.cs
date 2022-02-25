using System.Collections.Generic;
using System.IO;
using Puerts;
using UnityEngine;

namespace DefaultNamespace
{
	public class NeedleLoader : ILoader
	{
		private readonly List<string> directories = new List<string>();

		public string debugDirectory;

		public void AddFolder(string dir)
		{
			this.directories.Add(dir);
		}

		public void RemoveFolder(string dir)
		{
			this.directories.Remove(dir);
		}

		public bool FileExists(string filepath)
		{
			foreach (var dir in directories)
			{
				if(File.Exists(Path.Combine(dir, filepath))) return true;
			}
			
			var pathToUse = PathToUse(filepath);
			var exist = Resources.Load(pathToUse) != null;
#if !PUERTS_GENERAL && UNITY_EDITOR && !UNITY_2018_1_OR_NEWER
            if (!exist) 
            {
                UnityEngine.Debug.LogWarning("【Puerts】unity 2018- is using, if you found some js is not exist, rename *.cjs,*.mjs in the resources dir with *.cjs.txt,*.mjs.txt");
            }
#endif
			return exist;
		}

		public string ReadFile(string filepath, out string debugpath)
		{
			foreach (var dir in directories)
			{
				var fp = Path.Combine(dir, filepath);
				if (File.Exists(fp))
				{
					debugpath = Path.Combine(fp, filepath);
					return File.ReadAllText(debugpath);
				}
			}
			
			var pathToUse = PathToUse(filepath);
			var file = (TextAsset)Resources.Load(pathToUse);

			if (string.IsNullOrWhiteSpace(debugDirectory) || !Directory.Exists(debugDirectory))
				debugDirectory = Application.dataPath;
			debugpath = Path.Combine(debugDirectory, filepath);
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
			debugpath = debugpath.Replace("/", "\\");
#endif
			return file == null ? null : file.text;
		}

		private static string PathToUse(string filepath)
		{
			return 
				// .cjs asset is only supported in unity2018+
#if UNITY_2018_1_OR_NEWER
				filepath.EndsWith(".cjs") || filepath.EndsWith(".mjs")  ? 
					filepath.Substring(0, filepath.Length - 4) : 
#endif
					filepath;
		}
	}
}