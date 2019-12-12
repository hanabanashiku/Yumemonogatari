using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Yumemonogatari.Interactions {
    /// <summary>
    /// Represents a scripted interaction.
    /// </summary>
    [Serializable]
    public class InteractionScript {
        /// <summary>
        /// The trigger to start the interaction.
        /// </summary>
        [XmlElement("onActivateTrigger", typeof(OnActivateTrigger))]
        [XmlElement("onNpcDeathTrigger", typeof(OnNpcDeathTrigger))]
        [XmlElement("onPlayerEnterTrigger", typeof(OnPlayerEnterTrigger))]
        [XmlElement("onSceneLoadedTrigger", typeof(OnSceneLoadedTrigger))]
        public Trigger trigger;

        // the list of actions
        [XmlArray("actions")]
        [XmlArrayItem("dialogueAction", typeof(DialogueAction))]
        [XmlArrayItem("movementAction", typeof(MovementAction))]
        [XmlArrayItem("spawnAction", typeof(SpawnAction))]
        public List<ScriptedAction> actions;

        private Queue<ScriptedAction> _actionQueue;

        /// <summary>
        /// Start running the script
        /// </summary>
        public void Start() {
            if(actions.Count == 0) {
                Debug.LogWarning("Action queue is empty.");
                return;
            }

            if(_actionQueue != null) {
                Debug.LogWarning("The script is already running");
                return;
            }
            
            _actionQueue = new Queue<ScriptedAction>(actions);
            Next();
        }

        /// <returns>True if there is another action to perform</returns>
        public bool HasNext() => _actionQueue != null && _actionQueue.Count > 0;

        /// <summary>
        /// Perform the next action.
        /// </summary>
        public void Next() {
            if(!HasNext())
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