using System.IO;

namespace Needle.Puerts
{
	public class TypescriptWatcher
	{
		private FileSystemWatcher watcher;
		private string dir;


		public void BeginWatch(string dir)
		{
			if (dir == this.dir && this.watcher != null) return;
			StopWatch();
			this.dir = dir;
			watcher ??= new FileSystemWatcher();
			watcher.Path = this.dir;
			watcher.Filter = "*.ts";
			watcher.IncludeSubdirectories = true;
			watcher.NotifyFilter = NotifyFilters.LastWrite;
			watcher.Changed += OnChange;
			watcher.Renamed += OnChange;
			watcher.Deleted += OnDeleted;
			watcher.EnableRaisingEvents = true;
		}

		private void OnDeleted(object sender, FileSystemEventArgs e)
		{
		}

		private void OnChange(object sender, FileSystemEventArgs e)
		{
			TypescriptHandler.CompileTypescript(e.FullPath);
		}

		public void StopWatch()
		{
			watcher.EnableRaisingEvents = false;
			watcher.Dispose();
			watcher = null;
		}
	}
}