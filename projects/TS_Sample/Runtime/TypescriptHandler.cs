using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Needle.Puerts
{
	public static class TypescriptHandler
	{
#if UNITY_EDITOR
		[InitializeOnLoadMethod]
		public static void Init()
		{
			EditorApplication.update += () =>
			{
				if (Application.isPlaying && Time.frameCount % 20 == 0)
					AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
			};
		}
#endif

		public static async void CompileTypescript(string path)
		{
			var fullPath = Path.GetFullPath(path);
			if (string.IsNullOrEmpty(fullPath) || !File.Exists(fullPath))
			{
				Debug.LogError("File does not exist?: " + fullPath);
				return;
			}
			var pi = new ProcessStartInfo();
			pi.WorkingDirectory = Path.GetDirectoryName(fullPath)!;
			pi.FileName = "cmd.exe";
			pi.Arguments = $"/c tsc";// {Path.GetFileName(fullPath)} --target esnext --outfile TESTESTEST.js && timeout 30"; // + Path.GetFileName(fullPath) + " --project " + pi.WorkingDirectory + "  && timeout 10";
			pi.CreateNoWindow = true;
			pi.UseShellExecute = false;
			Debug.Log("Compile: " + Path.GetFileName(fullPath) + ": \"" + pi.Arguments + "\"");
			var proc = new Process();
			proc.StartInfo = pi;
			proc.Start();
			while (!proc.HasExited) await Task.Delay(5);
#if UNITY_EDITOR
			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
#endif

			RuntimeHandler.ReloadComponent(Path.GetFileNameWithoutExtension(path));
		}
	}
}