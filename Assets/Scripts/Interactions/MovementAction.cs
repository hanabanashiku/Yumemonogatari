using System;
using UnityEngine;

namespace Yumemonogatari.Interactions {
    [Serializable]
    public class MovementAction : ScriptedAction {
        public string identifier;
        public Vector2 location;
        public bool run;

        public override ActionTypes Type => ActionTypes.Movement;

        public override void Perform() {
            var npc = GameManager.FindNpc(identifier);
            if(npc is null)
                return;
            
            npc.MoveTo(location, run);
        }
    }
}