using System;
using System.Collections.Generic;

namespace Yumemonogatari.Interactions {
    /// <summary>
    /// Represents a scripted interaction.
    /// </summary>
    [Serializable]
    public class InteractionScript {
        /// <summary>
        /// The trigger to start the interaction.
        /// </summary>
        public Trigger Trigger { get; protected set; }

        // the list of actions
        private Queue<ScriptedAction> _actions;

        /// <summary>
        /// Start running the script
        /// </summary>
        public void Start() {
            throw new NotImplementedException();
        }
    }
}