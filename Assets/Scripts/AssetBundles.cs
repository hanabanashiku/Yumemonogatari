using System.IO;
using UnityEngine;

namespace Yumemonogatari {
    public static class AssetBundles {
        private static AssetBundle _level;
        private static AssetBundle _items;
        private static AssetBundle _ui;
        private static AssetBundle _spawns;
        
        public static AssetBundle Level {
            get {
                FetchBundle("leveldata", ref _level);
                return _level;
            }
        } 
        
        public static AssetBundle Item {
            get {
                FetchBundle("items", ref _items);
                return _items;
            }
        }

        public static AssetBundle Ui {
            get {
                FetchBundle("ui", ref _ui);
                return _ui;
            }
        }

        public static AssetBundle Spawns {
            get {
                FetchBundle("spawns", ref _spawns);
                return _spawns;
            }
        }

        private static void FetchBundle(string name, ref AssetBundle bundle) {
            if(bundle != null)
                return;
            bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, name));
        }
    }
}