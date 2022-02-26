using System.Collections;
using System.Collections.Generic;
using Puerts;

namespace Needle.Puerts.Loaders
{
	public class CompoundLoader : ILoader
	{
		private readonly List<ILoader> loaders = new List<ILoader>();

		public void AddLoader(ILoader loader)
		{
			if (loaders.Contains(loader)) return;
			loaders.Add(loader);
		}

		public void RemoveLoader(ILoader loader)
		{
			loaders.Remove(loader);
		}

		public CompoundLoader(params ILoader[] loaders)
		{
			this.loaders.AddRange(loaders);
		}

		public bool FileExists(string filepath)
		{
			foreach (var l in loaders)
			{
				if (l.FileExists(filepath)) return true;
			}
			return false;
		}

		public string ReadFile(string filepath, out string debugPath)
		{
			foreach (var l in loaders)
			{
				var res = l.ReadFile(filepath, out debugPath);
				if (!string.IsNullOrWhiteSpace(res)) return res;
			}
			debugPath = null;
			return null;
		}
	}
}