using System;
using System.Collections.Generic;
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
        public SettingsManager.TextSpeeds textSpeed;
        public string currentScene;
        public int currentLevel;
        public Vector2 currentPosition;
        public Dictionary<Item, int> items;
        public int mon;
        public int bullets;
        public int arrows;
        public int remainingAmmo;
        public MeleeWeapon equippedMelee;
        public RangedWeapon equippedRanged;
        public Armor equippedArmor;
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
            var settings = SettingsManager.Instance;
            settings.textSpeed = textSpeed;

            var character = Object.FindObjectOfType<PlayerCharacter>();
            Debug.Assert(character != null);
            character.transform.position = currentPosition;
            
            var inventory = character.gameObject.AddComponent<Inventory>();
            character.inventory = inventory;
            inventory.items = items;
            inventory.mon = mon;
            inventory.bullets = bullets;
            inventory.arrows = arrows;
            inventory.EquipWeapon(equippedMelee);
            inventory.EquipWeapon(equippedRanged);
            inventory.EquipArmor(equippedArmor);
            inventory.remainingAmmo = remainingAmmo;
            
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