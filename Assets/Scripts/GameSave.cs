using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Yumemonogatari.Items;

namespace Yumemonogatari {
    [Serializable]
    public class GameSave {
        public DateTime time;
        public SettingsManager settings;
        public string currentScene;
        public int currentLevel;
        public Vector2 currentPosition;
        public Inventory inventory;
        public int health;
        public int shield;

        public void Serialize(string path) {
            var formatter = new BinaryFormatter();
            using(var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)) {
                formatter.Serialize(stream, this);
            }
        }
    }
}