using System;
using UnityEngine;
using Yumemonogatari.UI;

namespace Yumemonogatari.Interactions {
    /// <summary>
    /// The manager for scripted interactions.
    /// </summary>
    public class InteractionManager : MonoBehaviour {
        public static InteractionManager Instance { get; private set; }
        
        /// <summary>
        /// The UI dialogue box controller.
        /// </summary>
        public DialogueBoxController DialogueBox { get; private set; }

        public delegate void OnActionCompleted();

        private void Awake() {
            if(Instance is null)
                Instance = this;
        }

        private void Start() {
            DialogueBox = FindObjectOfType<DialogueBoxController>();
        }

        public void OnDialogueCompleted() {
            throw new NotImplementedException();
        }
        
    }
}