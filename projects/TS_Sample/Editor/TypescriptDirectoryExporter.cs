using System;
using System.Collections.Generic;
using System.IO;
using Needle.Puerts.Loaders;
using PlasticGui.WorkspaceWindow.Items;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Needle.Puerts
{
	public class TypescriptDirectoryExporter : IPreprocessBuildWithReport, IPostprocessBuildWithReport
	{
		public int callbackOrder { get; }
		private static readonly List<TypescriptDirectory> directories = new List<TypescriptDirectory>();

		public void OnPreprocessBuild(BuildReport report)
		{
			directories.Clear();
			if (RuntimeHandler.Exists)
			{
				directories.AddRange(RuntimeHandler.Instance.ScriptLoaders);
			}
		}

		public void OnPostprocessBuild(BuildReport report)
		{
			if (directories.Count > 0)
				MoveScriptsToStreamingAssets(report.summary.outputPath);
			directories.Clear();
		}

		private static void MoveScriptsToStreamingAssets(string buildPath)
		{
			if (directories.Count <= 0) return;
			var directory = Path.GetDirectoryName(buildPath);
			var dataDirectory = directory + "/" + Path.GetFileNameWithoutExtension(buildPath) + "_Data";
			var streamingAssetsDirectory = dataDirectory + "/StreamingAssets";
			foreach (var scr in directories)
			{
				var path = AssetDatabase.GetAssetPath(scr);
				var dir = Path.GetDirectoryName(path);
				if (dir == null) continue;
				var streamingAssetsRelativePath = "";
				if (path.StartsWith("Assets/"))
				{
					var p0 = new Uri(Path.GetFullPath(Application.dataPath), UriKind.Absolute);
					var p1 = new Uri(Path.GetFullPath(dir));
					streamingAssetsRelativePath = p0.MakeRelativeUri(p1).ToString().Substring("Assets/".Length);
				}
				else if (path.StartsWith("Packages/"))
				{
					var packageName = path.Substring("Packages/".Length);
					var nameEnd = packageName.IndexOf("/", StringComparison.Ordinal);
					packageName = packageName.Substring(0, nameEnd);
					streamingAssetsRelativePath = packageName + "/" + new DirectoryInfo(dir).Name;
				}
				if (!string.IsNullOrWhiteSpace(streamingAssetsRelativePath))
				{
					var relDir = "StreamingAssets/" + streamingAssetsRelativePath;
					scr.scriptsDirectory = relDir.Replace("\\", "/");
					var typescriptFile = new FileInfo(path);
					var pathInStreamingAssets = dataDirectory + "/StreamingAssets/" + streamingAssetsRelativePath;
					Directory.CreateDirectory(pathInStreamingAssets);
					CopyDirectory(Path.GetFullPath(dir), pathInStreamingAssets, true,
						entry =>
						{
							if (entry.FullName == typescriptFile.FullName) return true;
							if (entry.Name.EndsWith(".meta")) return true;
							if (entry.Name.EndsWith(".cs")) return true;
							return false;
						});
				}
				else Debug.LogWarning("Did not export " + path);
			}

			var tsConfigPath = streamingAssetsDirectory + "/tsconfig.json";
			if(!File.Exists(tsConfigPath))
			{
				Debug.Log("Create tsconfig: " + tsConfigPath);
				MenuItems.CreateTsConfig(streamingAssetsDirectory);
			}
		}

		private static void CopyDirectory(string sourceDir, string destinationDir, bool recursive, Predicate<FileSystemInfo> filter = null)
		{
			var dir = new DirectoryInfo(sourceDir);
			var dirs = dir.GetDirectories();
			Directory.CreateDirectory(destinationDir);
			foreach (var file in dir.GetFiles())
			{
				if (filter?.Invoke(file) ?? false) continue;
				var targetFilePath = Path.Combine(destinationDir, file.Name);
				file.CopyTo(targetFilePath);
			}
			if (recursive)
			{
				foreach (var subDir in dirs)
				{
					if (filter?.Invoke(subDir) ?? false) continue;
					var newDestinationDir = Path.Combine(destinationDir, subDir.Name);
					CopyDirectory(subDir.FullName, newDestinationDir, true, filter);
				}
			}
		}
	}
}