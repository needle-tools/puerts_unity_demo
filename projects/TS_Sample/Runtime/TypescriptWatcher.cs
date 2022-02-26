using System.IO;
using UnityEngine;

namespace Needle.Puerts
{
	public class TypescriptWatcher
	{
		private FileSystemWatcher watcher;
		private string dir;

		public TypescriptWatcher(string dir = null)
		{
			if (dir != null) BeginWatch(dir);
		}

		public void BeginWatch(string directory)
		{
			if (directory == this.dir && this.watcher != null) return;
			StopWatch();
			Debug.Log("Begin Watching " + directory);
			this.dir = directory;
			watcher ??= new FileSystemWatcher();
			watcher.Path = this.dir;
			watcher.Filter = "*.ts";
			watcher.IncludeSubdirectories = true;
			watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
			watcher.Changed += OnChange;
			watcher.Renamed += OnChange;
			watcher.Deleted += OnDeleted;
			watcher.EnableRaisingEvents = true;
		}

		private void OnDeleted(object sender, FileSystemEventArgs e)
		{
			Debug.Log("Script deleted: " + e.FullPath);
		}

		private void OnChange(object sender, FileSystemEventArgs e)
		{
			Debug.Log("Script changed: " + e.FullPath);
			TypescriptHandler.CompileTypescript(e.FullPath);
		}

		public void StopWatch()
		{
			if (watcher != null)
			{
				watcher.EnableRaisingEvents = false;
				watcher.Dispose();
				watcher = null;
			}
		}
	}
}