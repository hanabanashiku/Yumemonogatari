using System;

namespace Yumemonogatari.Interactions {
    [Serializable]
    public abstract class ScriptedAction {

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
            LoadResource
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