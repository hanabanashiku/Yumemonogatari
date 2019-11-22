using System;

namespace Yumemonogatari.Interactions {
    /// <summary>
    /// An action that spawns a character, enemy, or group of enemies.
    /// </summary>
    [Serializable]
    public class SpawnAction : ScriptedAction {
        public override ActionTypes Type => ActionTypes.Spawn;
        
        public override void Perform() {
            throw new System.NotImplementedException();
        }
    }
}