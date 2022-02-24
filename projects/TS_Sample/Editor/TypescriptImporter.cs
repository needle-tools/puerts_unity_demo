using System.Diagnostics;
using System.IO;
using UnityEditor.AssetImporters;
using Debug = UnityEngine.Debug;

namespace Editor
{
	[ScriptedImporter(0, ".ts")]
	public class TypescriptImporter : ScriptedImporter
	{
		public override void OnImportAsset(AssetImportContext ctx)
		{
			if (ctx.assetPath.EndsWith(".ts"))
			{
				CompileTypescript(ctx.assetPath);
			}
		}

		private void CompileTypescript(string path)
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
			pi.Arguments = "/c tsc ";// + Path.GetFileName(fullPath) + " --project " + pi.WorkingDirectory + "  && timeout 10";
			Debug.Log("Compile: " + Path.GetFileName(fullPath) + ": \"" + pi.Arguments + "\"");
			var proc = new Process();
			proc.StartInfo = pi;
			proc.Start();
		}
	}
}