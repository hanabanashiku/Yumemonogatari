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

        private Queue<ScriptedAction> _clone;

        /// <summary>
        /// Start running the script
        /// </summary>
        public void Start() {
            _clone = new Queue<ScriptedAction>(_actions);
            Next();
        }

        /// <returns>True if there is another action to perform</returns>
        public bool HasNext() => _clone != null && _clone.Count > 0;

        /// <summary>
        /// Perform the next action.
        /// </summary>
        public void Next() {
            var action = _clone.Dequeue();
            action.Perform();
        }
    }
}