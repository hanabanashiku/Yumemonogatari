using System;
using UnityEngine;

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
    
        public GameObject pauseMenuPrefab;
        public GameObject hud;
    
        private void Awake() {
            Instance = this;
            InputManager = GetComponentInChildren<InputManager>();
        }

        private void Update() {
            if(Input.GetButtonUp("Pause") && Math.Abs(Time.timeScale) > 0.01)
                Pause();
        }
    
        private void Pause() {
            Instantiate(pauseMenuPrefab);
            hud.SetActive(false);
        }
    }
}
