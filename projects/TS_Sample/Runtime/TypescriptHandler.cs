using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

		public static async void CompileTypescript(string path, bool debugLog)
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
			pi.Arguments = $"/c tsc"; // {Path.GetFileName(fullPath)} --target esnext --outfile TESTESTEST.js && timeout 30"; // + Path.GetFileName(fullPath) + " --project " + pi.WorkingDirectory + "  && timeout 10";
			EnsureTemporaryTsConfig(pi, fullPath, pi.WorkingDirectory);
			pi.CreateNoWindow = true;
			pi.UseShellExecute = false;
			if (debugLog)
				Debug.Log("Compile: " + Path.GetFileName(fullPath) + ": \"" + pi.Arguments + "\"");
			var proc = new Process();
			proc.StartInfo = pi;
			proc.Start();
			while (!proc.HasExited) await Task.Delay(5);
			// if (!string.IsNullOrWhiteSpace(tsConfigPath) && File.Exists(tsConfigPath)) File.Delete(tsConfigPath);
#if UNITY_EDITOR
			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
#endif

			RuntimeHandler.ReloadComponent(Path.GetFileNameWithoutExtension(path));
		}

		private static void EnsureTemporaryTsConfig(ProcessStartInfo si, string scriptPath, string scriptDirectory)
		{
			if (!TryFindTsConfigInParentDirectory(new DirectoryInfo(scriptDirectory)))
			{
				Debug.LogError("Missing tsconfig to compile " + scriptPath);
// #if UNITY_EDITOR
// 				var basePath = Path.GetFullPath($"{Application.dataPath}/../Temp");
// 				var tsConfigPath = $"{basePath}/tsconfig-{DateTime.Now.ToFileTime()}.json";
// 				var templatePath = UnityEditor.AssetDatabase.GUIDToAssetPath("1e1d43992ed743b8a195982130bea04c");
// 				File.Copy(templatePath, tsConfigPath, true);
// 				si.Arguments += " -project " + tsConfigPath + " && timeout 30";
// #endif
			}
		}

		private static bool TryFindTsConfigInParentDirectory(DirectoryInfo dir)
		{
			if (dir == null) return false;
			if (dir.EnumerateFiles().Any(file => file.Name == "tsconfig.json"))
				return true;
			return TryFindTsConfigInParentDirectory(dir.Parent);
		}
	}
}