using UnityEditor;
using System.IO;

namespace Yumemonogatari.Editor {
    public class AssetBundles {
        [MenuItem("Assets/Build Asset Bundles")]
        static void BuildAllAssetBundles() {
            const string dir = "Assets/StreamingAssets";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            BuildPipeline.BuildAssetBundles(dir,
                BuildAssetBundleOptions.ChunkBasedCompression,
                EditorUserBuildSettings.activeBuildTarget);
        }
    }
}