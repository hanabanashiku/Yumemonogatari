using System;
using System.Linq;
using UnityEngine;
using Yumemonogatari.Entities;
using Yumemonogatari.Interactions;

namespace Yumemonogatari {
    public class GameManager : MonoBehaviour {
        
        /// <summary>
        /// The current running game manager
        /// </summary>
        public static GameManager Instance { get; private set; }
        
        /// <summary>
        /// The input manager.
        /// </summary>
        public static InputManager InputManager { get; private set; }
        
        public static InteractionManager InteractionManager { get; private set; }
    
        public GameObject pauseMenuPrefab;
        public GameObject hud;
    
        private void Awake() {
            Instance = this;
            InputManager = gameObject.AddComponent<InputManager>();
            InteractionManager = gameObject.AddComponent<InteractionManager>();
        }

        private void Update() {
            // only pause when timescale is not 0,
            // preventing multiple pause menus or resetting the scale when dialogue is shown.
            if(Input.GetButtonUp("Pause") && Math.Abs(Time.timeScale) > 0.01)
                Pause();
        }
    
        private void Pause() {
            Instantiate(pauseMenuPrefab);
            hud.SetActive(false);
        }

        /// <summary>
        /// Find an NPC, enemy, or object that has been instantiated.
        /// </summary>
        /// <param name="identifier">The identifier to find by.</param>
        /// <returns>The character, or null.</returns>
        public static Character FindNpc(string identifier) {
            return FindObjectsOfType<Character>().FirstOrDefault(x => x.identifier.Equals(identifier));
        }
    }
}
