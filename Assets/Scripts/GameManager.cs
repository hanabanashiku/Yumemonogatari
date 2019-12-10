using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yumemonogatari.Entities;
using Yumemonogatari.Interactions;
using Yumemonogatari.UI;

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
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void Start() {
            hud = FindObjectOfType<HudController>()?.gameObject;
        }

        private void Update() {
            // only pause when timescale is not 0,
            // preventing multiple pause menus or resetting the scale when dialogue is shown.
            if(Input.GetButtonUp("Pause") && Math.Abs(Time.timeScale) > 0.01)
                Pause();
        }

        private static void OnSceneChanged(Scene current, Scene next) {
            InteractionManager.Instance.SceneLoaded();
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

        /// <summary>
        /// Save the game
        /// </summary>
        /// <param name="save">The save number to save.</param>
        public static void SaveGame(int save = -1) {
            if(save == -1) {
                var files = Directory.GetFiles(Application.persistentDataPath, "*.sav");
                for(var i = 0; ; i++)
                    if(!files.Contains($"{i}.sav")) {
                        save = i;
                        break;
                    }
            }
            var path = Path.Combine(Application.persistentDataPath, $"{save}.sav");
            var player = FindObjectOfType<PlayerCharacter>();

            var gameSave = new GameSave() {
                time = DateTime.Now,
                settings = SettingsManager.Instance,
                currentScene = SceneManager.GetActiveScene().name,
                currentLevel = InteractionManager.CurrentLevel.number,
                currentPosition = player.gameObject.transform.position,
                inventory = player.inventory,
                health = player.health,
                shield = player.shield
            };
            gameSave.Serialize(path);
        }

        public static void SetUpScene() {
            var canvas = AssetBundles.Ui.LoadAsset<GameObject>("HUDCanvas");
            canvas = Instantiate(canvas);
            DontDestroyOnLoad(canvas);
            var manager = AssetBundles.Ui.LoadAsset<GameObject>("GameManager");
            manager = Instantiate(manager);
            DontDestroyOnLoad(manager);
            var player = AssetBundles.Spawns.LoadAsset<GameObject>("Player");
            player = Instantiate(player);
            DontDestroyOnLoad(player);
        }
    }
}
