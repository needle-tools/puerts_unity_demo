using System.IO;
using Puerts;
using UnityEngine;

namespace DefaultNamespace
{
	public class NeedleLoader : ILoader
	{
		private readonly string root = "";

		public NeedleLoader()
		{
		}

		public NeedleLoader(string root)
		{
			this.root = Path.GetFullPath(root);
		}

		private string PathToUse(string filepath)
		{
			return 
				// .cjs asset is only supported in unity2018+
#if UNITY_2018_1_OR_NEWER
				filepath.EndsWith(".cjs") || filepath.EndsWith(".mjs")  ? 
					filepath.Substring(0, filepath.Length - 4) : 
#endif
					filepath;
		}

		public bool FileExists(string filepath)
		{
			if(File.Exists(Path.Combine(root, filepath))) return true;
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
			if (File.Exists(Path.Combine(root, filepath)))
			{
				debugpath = Path.Combine(root, filepath);
				return File.ReadAllText(debugpath);
			}
			
			var pathToUse = PathToUse(filepath);
			var file = (TextAsset)Resources.Load(pathToUse);
            
			debugpath = Path.Combine(root, filepath);
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
			debugpath = debugpath.Replace("/", "\\");
#endif
			return file == null ? null : file.text;
		}
	}
}