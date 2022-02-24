using UnityEditor.AssetImporters;

namespace Editor
{
	[ScriptedImporter(0, ".ts")]
	public class TypescriptImporter : ScriptedImporter
	{
		public override void OnImportAsset(AssetImportContext ctx)
		{
			if (ctx.assetPath.EndsWith(".ts"))
			{
				ReloadHandler.CompileTypescript(ctx.assetPath);
			}
		}
	}
}