using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Yumemonogatari.Interactions {
    /// <summary>
    /// An action that spawns a character, enemy, or group of enemies.
    /// </summary>
    [Serializable]
    public class SpawnAction : ScriptedAction {
        public override ActionTypes Type => ActionTypes.Spawn;
        
        public Vector2 location;
        public string identifier;
        
        public override void Perform() {
            
            var obj = AssetBundles.Spawns.LoadAsset<GameObject>(identifier);
            Object.Instantiate(obj);
            obj.transform.position = location;
        }
    }
}