using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Needle.Puerts;
using UnityEditor;
using Debug = UnityEngine.Debug;


public static class ReloadHandler
{
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
		pi.Arguments = "/c tsc ";// + Path.GetFileName(fullPath) + " --project " + pi.WorkingDirectory + "  && timeout 10";
		pi.CreateNoWindow = true;
		pi.UseShellExecute = false;
		Debug.Log("Compile: " + Path.GetFileName(fullPath) + ": \"" + pi.Arguments + "\"");
		var proc = new Process();
		proc.StartInfo = pi;
		proc.Start();
		while (proc != null && !proc.HasExited)
		{
			await Task.Delay(100);
		}
		await Task.Delay(100);
		AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
		RuntimeHandler.ReloadAllComponents();
	}
}