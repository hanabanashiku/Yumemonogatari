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
        public Trigger trigger;

        // the list of actions
        public List<ScriptedAction> actions;

        private Queue<ScriptedAction> _actionQueue;

        /// <summary>
        /// Start running the script
        /// </summary>
        public void Start() {
            _actionQueue = new Queue<ScriptedAction>(actions);
            Next();
        }

        /// <returns>True if there is another action to perform</returns>
        public bool HasNext() => _actionQueue != null && _actionQueue.Count > 0;

        /// <summary>
        /// Perform the next action.
        /// </summary>
        public void Next() {
            if(_actionQueue is null)
                throw new InvalidOperationException();
            var action = _actionQueue.Dequeue();
            action.Perform();
            if(_actionQueue.Count == 0)
                _actionQueue = null;
        }

        public InteractionScript() {
            actions = new List<ScriptedAction>();
        }
    }
}