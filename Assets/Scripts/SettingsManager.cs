using System;
using UnityEngine;

namespace Yumemonogatari {
    [Serializable]
    public class SettingsManager : MonoBehaviour {
        private static SettingsManager _instance;

        public static SettingsManager Instance {
            get {
                if(_instance is null)
                    _instance = GameManager.Instance.gameObject.AddComponent<SettingsManager>();
                return _instance;
            }
            private set => _instance = value;
        }

        public enum TextSpeeds {
            /// <summary>3 cps</summary>
            Slow = 3, 
            /// <summary>5 cps</summary>
            Medium = 5, 
            /// <summary>7 cps</summary>
            Fast = 7
        }

        public TextSpeeds textSpeed;

        private void Awake() {
            if(Instance == null)
                Instance = this;
        }
    }
}