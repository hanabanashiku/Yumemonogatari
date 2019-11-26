using System;
using UnityEditor;
using UnityEngine;

namespace Yumemonogatari.Interactions {
    /// <summary>
    /// An action that spawns a character, enemy, or group of enemies.
    /// </summary>
    [Serializable]
    public class SpawnAction : ScriptedAction {
        public override ActionTypes Type => ActionTypes.Spawn;
        
        public Vector2 Location;
        public string Prefab;
        
        public override void Perform() {
            var obj = AssetBundles.Spawns.LoadAsset<GameObject>(Prefab);
            Instantiate(obj);
            obj.transform.position = Location;
        }
    }
}