using System;

namespace Yumemonogatari.Interactions {
    /// <summary>
    /// A trigger to start running an InteractionScript.
    /// </summary>
    [Serializable]
    public abstract class Trigger {
        public enum TriggerTypes {
            /// <summary>
            /// The player has inspected an object or NPC.
            /// </summary>
            OnActivate,
            /// <summary>
            /// The player has loaded a new scene.
            /// </summary>
            OnSceneLoaded,
            /// <summary>
            /// An enemy has been defeated.
            /// </summary>
            OnNpcDeath,
            /// <summary>
            /// The player enters a predefined Bounds on a given scene.
            /// </summary>
            OnPlayerEnter
        }
        
        /// <summary>
        /// The type of trigger.
        /// </summary>
        public virtual TriggerTypes Type { get; }

        /// <summary>
        /// Should the trigger be activated?
        /// </summary>
        /// <returns>True if the trigger should be activated.</returns>
        public abstract bool ConditionIsMet();
    }
}