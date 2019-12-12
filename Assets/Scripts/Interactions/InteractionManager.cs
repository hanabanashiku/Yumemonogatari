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
        /// <summary>
        /// The current level.
        /// </summary>
        public Level CurrentLevel { get; private set; }
        
        /// <summary>
        /// Get all of the default interactions.
        /// </summary>
        public static Level DefaultLevel { get; private set; }

        private InteractionScript _currentScript;

        public delegate void OnActionCompleted();

        public void LoadLevel(int level) {
            CurrentLevel = Level.LoadLevel(level);
        }

        public void NpcDeath(string identifier) {
            InteractionScript script = null;

            if(CurrentLevel != null)
                script = CurrentLevel.GetDeathTrigger(identifier);
            if(script == null)
                DefaultLevel.GetDeathTrigger(identifier);
            if(script == null)
                return;

            _currentScript = script;
            _currentScript.Start();
        }

        public void ObjectActivated(string identifier) {
            InteractionScript script = null;

            if(CurrentLevel != null)
                script = CurrentLevel.GetActivationTrigger(identifier);
            if(script == null)
                DefaultLevel.GetActivationTrigger(identifier);
            if(script == null)
                return;

            _currentScript = script;
            _currentScript.Start();
        }

        public void SceneLoaded() {
            InteractionScript script = null;

            if(CurrentLevel != null)
                script = CurrentLevel.GetSceneTrigger();
            if(script == null)
                return;

            _currentScript = script;
            _currentScript.Start();
        }

        public void ActionCompleted() {
            if(_currentScript.HasNext())
                _currentScript.Next();
            else
                _currentScript = null;
        }
        
        private void Awake() {
            if(Instance is null)
                Instance = this;
            if(DefaultLevel is null)
                DefaultLevel = Level.LoadLevel(0);
        }

        private void Start() {
            DialogueBox = FindObjectOfType<DialogueBoxController>();
        }

        // check if the player has intersected a trigger on the map.
        private void Update() {
            if(CurrentLevel == null || !CurrentLevel.HasPlayerEnterTriggers)
                return;

            var script = CurrentLevel.GetPlayerEnterTrigger();
            if(script == null)
                return;
            _currentScript = script;
            script.Start();
        }
    }
}