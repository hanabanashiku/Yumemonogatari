using System;
using UnityEngine;

namespace Yumemonogatari {
    [Serializable]
    public class SettingsManager : MonoBehaviour {
        public static SettingsManager Instance;

        public enum TextSpeeds {
            /// <summary>3 cps</summary>
            Slow = 3, 
            /// <summary>5 cps</summary>
            Medium = 5, 
            /// <summary>7 cps</summary>
            Fast = 7,
            /// <summary>
            /// Infinite cps
            /// </summary>
            Immediate = 0
        }

        public TextSpeeds textSpeed = TextSpeeds.Immediate;

        private void Awake() {
            if(Instance == null)
                Instance = this;
            
            
        }
    }
}