using UnityEditor;
using System.IO;

namespace Yumemonogatari.Editor {
    public class AssetBundles {
        [MenuItem("Assets/Build Asset Bundles")]
        static void BuildAllAssetBundles() {
            var dir = "Assets/Bundles";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            BuildPipeline.BuildAssetBundles(dir,
                BuildAssetBundleOptions.None,
                BuildTarget.NoTarget);
        }
    }
}