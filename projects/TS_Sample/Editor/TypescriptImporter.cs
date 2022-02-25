using UnityEditor.AssetImporters;

namespace Needle.Puerts
{
	[ScriptedImporter(0, ".ts")]
	public class TypescriptImporter : ScriptedImporter
	{
		public override void OnImportAsset(AssetImportContext ctx)
		{
			if (ctx.assetPath.EndsWith(".ts"))
			{
				TypescriptHandler.CompileTypescript(ctx.assetPath);
			}
		}
	}
}