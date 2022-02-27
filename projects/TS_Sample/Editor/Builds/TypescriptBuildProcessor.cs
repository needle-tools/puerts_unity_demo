using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Needle.Puerts.Loaders;
using PlasticGui.WorkspaceWindow.Items;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Needle.Puerts
{
	public class TypescriptBuildProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
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
			{
				if (MoveScriptsToStreamingAssets(report.summary.outputPath, out var outputDirectories))
				{
					CreateTsConfigs(outputDirectories);
				}
			}
			directories.Clear();
		}

		private static bool MoveScriptsToStreamingAssets(string buildPath, out List<string> rootOutputDirectories)
		{
			rootOutputDirectories = null;
			if (directories.Count <= 0) return false;
			var directory = Path.GetDirectoryName(buildPath);
			var dataDirectory = directory + "/" + Path.GetFileNameWithoutExtension(buildPath) + "_Data";
			var streamingAssetsDirectory = dataDirectory + "/StreamingAssets";
			var exported = 0;
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
					exported += 1;
					var relDir = "StreamingAssets/" + streamingAssetsRelativePath;
					scr.scriptsDirectory = relDir.Replace("\\", "/");
					var typescriptFile = new FileInfo(path);
					var pathInStreamingAssets = dataDirectory + "/StreamingAssets/" + streamingAssetsRelativePath;
					Directory.CreateDirectory(pathInStreamingAssets);
					CopyDirectory(Path.GetFullPath(dir), pathInStreamingAssets, true,
						entry =>
						{
							if (entry.FullName == typescriptFile.FullName) return true;
							return DefaultFilter(entry);
						});
				}
				else Debug.LogWarning("Did not export " + path);
			}
			if (exported > 0)
			{
				rootOutputDirectories = new List<string>();
				rootOutputDirectories.Add(streamingAssetsDirectory);
			}
			return exported > 0;
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

		private static void CreateTsConfigs(List<string> outputDirectories)
		{
			// TODO: automatically find typings
			var typesDirectory = new string[]
			{
				Application.dataPath + "/Gen/Typing",
				"Packages/com.tencent.puerts/Runtime/Puerts/Typing"
			};
			foreach (var dir in outputDirectories)
			{
				var tsConfigPath = dir + "/tsconfig.json";
				if (!File.Exists(tsConfigPath))
				{
					Debug.Log("Create tsconfig: " + tsConfigPath);
					MenuItems.CreateTsConfig(dir, "");

					foreach (var td in typesDirectory)
					{
						if (!Directory.Exists(td))
						{
							Debug.LogWarning("Typings not found at " + td);
							continue;
						}
						var path = Path.GetFullPath(td);
						var outputDir = dir + "/Typing";
						Debug.Log("Copy typings from " + path + " to " + outputDir);
						CopyDirectory(path, outputDir, true, DefaultFilter);
					}
				}
			}
		}


		private static readonly string[] defaultAllowedExtensions =
		{
			".js",
			".ts",
			".js.map",
			".d",
		};
		
		private static bool DefaultFilter(FileSystemInfo entry)
		{
			if (entry is DirectoryInfo) return false;
			if (defaultAllowedExtensions.Any(e => entry.Name.EndsWith(e))) return false;
			return true;
		}
	}
}