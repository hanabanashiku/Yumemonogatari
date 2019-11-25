using System;
using UnityEngine;

namespace Yumemonogatari.Interactions {
    [Serializable]
    public abstract class ScriptedAction : MonoBehaviour {

        public enum ActionTypes {
            /// <summary>
            /// Display a chain of dialogue.
            /// </summary>
            Dialogue, 
            /// <summary>
            /// Make a character walk or run to a location.
            /// </summary>
            Movement, 
            /// <summary>
            /// Spawn a character, enemy, or group.
            /// </summary>
            Spawn, 
            /// <summary>
            /// Load a prefab resource at a given location.
            /// </summary>
            LoadResource,
            /// <summary>
            /// Add an item to the player's inventory.
            /// </summary>
            AddToInventory
        }

        private static event InteractionManager.OnActionCompleted ActionCompleted;

        protected ScriptedAction() {
            if(ActionCompleted is null)
                ActionCompleted += InteractionManager.Instance.ActionCompleted;
        }
        
        /// <summary>
        /// The type of action.
        /// </summary>
        public abstract ActionTypes Type { get; }
        
        /// <summary>
        /// Perform the action.
        /// </summary>
        public abstract void Perform();
    }
}