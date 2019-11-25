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
            var obj = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Prefabs/Spawns/{Prefab}.prefab");
            Instantiate(obj);
            obj.transform.position = Location;
        }
    }
}