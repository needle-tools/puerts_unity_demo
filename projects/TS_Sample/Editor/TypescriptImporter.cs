using UnityEditor.AssetImporters;

namespace Needle.Puerts
{
	[ScriptedImporter(0, ".ts")]
	public class TypescriptImporter : ScriptedImporter
	{
		public override void OnImportAsset(AssetImportContext ctx)
		{
			if (ctx.assetPath.EndsWith(".ts") && !ctx.assetPath.EndsWith("index.d.ts"))
			{
				TypescriptHandler.CompileTypescript(ctx.assetPath, false);
			}
		}
	}
}