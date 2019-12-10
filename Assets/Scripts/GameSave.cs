using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yumemonogatari.Entities;
using Yumemonogatari.Interactions;
using Yumemonogatari.Items;
using Yumemonogatari.UI;
using Object = UnityEngine.Object;

namespace Yumemonogatari {
    /// <summary>
    /// A game save state.
    /// </summary>
    [Serializable]
    public class GameSave {
        public int number;
        public DateTime time;
        public SettingsManager settings;
        public string currentScene;
        public int currentLevel;
        public Vector2 currentPosition;
        public Inventory inventory;
        public int health;
        public int shield;

        /// <summary>
        /// Serialize a game save.
        /// </summary>
        /// <param name="path">The location to save to.</param>
        public void Serialize(string path) {
            var formatter = new BinaryFormatter();
            using(var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)) {
                formatter.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Deserialize a game save and produce an object.
        /// </summary>
        /// <param name="path">The path to deserialize</param>
        /// <returns>The object.</returns>
        public static GameSave Deserialize(string path) {
            var formatter = new BinaryFormatter();
            using(var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                return (GameSave)formatter.Deserialize(stream);
        }

        /// <summary>
        /// Load the game save.
        /// </summary>
        public void Load() {
            SceneManager.LoadScene(currentScene);
            if(Object.FindObjectOfType<GameManager>() is null)
                GameManager.SetUpScene();
            
            InteractionManager.Instance.LoadLevel(currentLevel);
            SettingsManager.Instance = settings;

            var character = Object.FindObjectOfType<PlayerCharacter>();
            Debug.Assert(character != null);
            character.transform.position = currentPosition;
            character.inventory = inventory;
            character.health = health;
            character.shield = shield;

            // make sure the reference updates
            var weaponDisplay = Object.FindObjectOfType<WeaponDisplayController>();
            if(weaponDisplay != null) {
                weaponDisplay.inventory = inventory;
            }
        }
    }
}