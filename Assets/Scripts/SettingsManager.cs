using System;
using UnityEngine;

namespace Yumemonogatari {
    [Serializable]
    public class SettingsManager : MonoBehaviour {
        public static SettingsManager Instance { get; private set; }

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