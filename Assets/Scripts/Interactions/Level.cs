using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace Yumemonogatari.Interactions {
    [Serializable]
    public class Level {
        [XmlAttribute("number")]
        public int number;

        [XmlArray("actions")]
        [XmlArrayItem("action")]
        public List<InteractionScript> scripts;

        private bool? _playerEnterTriggers;

        /// <summary>
        /// Are there any bounding box triggers to test for?
        /// </summary>
        /// <remarks>This variable is to cache the result to reduce the number of unnecessary checks.</remarks>
        internal bool HasPlayerEnterTriggers {
            get {
                if(!_playerEnterTriggers.HasValue)
                    _playerEnterTriggers = scripts.Any(x => x.Trigger.GetType() == typeof(OnPlayerEnterTrigger));
                return _playerEnterTriggers.Value;
            }
        }

        /// <summary>
        /// Check for an OnNpcDeathTrigger.
        /// </summary>
        /// <param name="identifier">The identifier of the NPC</param>
        /// <returns>The trigger, or null.</returns>
        public InteractionScript GetDeathTrigger(string identifier) {
            return scripts.FirstOrDefault(x =>
                x.Trigger.GetType() == typeof(OnNpcDeathTrigger) &&
                ((OnNpcDeathTrigger)x.Trigger).Identifier.Equals(identifier));
        }
        
        /// <summary>
        /// Check for an OnActivateTrigger.
        /// </summary>
        /// <param name="identifier">The identifier of the NPC</param>
        /// <returns>The trigger, or null.</returns>
        public InteractionScript GetActivationTrigger(string identifier) {
            return scripts.FirstOrDefault(x =>
                x.Trigger.GetType() == typeof(OnActivateTrigger) &&
                ((OnActivateTrigger)x.Trigger).Identifier.Equals(identifier));
        }

        public InteractionScript GetPlayerEnterTrigger() {
            return scripts.FirstOrDefault(x =>
                x.Trigger.GetType() == typeof(OnPlayerEnterTrigger) &&
                ((OnPlayerEnterTrigger)x.Trigger).ConditionIsMet());
        }

        /// <summary>
        /// Load a set of level data.
        /// </summary>
        /// <param name="level">The level number.</param>
        /// <returns></returns>
        internal static Level LoadLevel(int level) {
            var asset = AssetBundles.Level.LoadAsset<TextAsset>($"{level}");
            if(asset is null) {
                Debug.LogWarning($"Could not find level {level}");
                return null;
            }
            var xml = new XmlSerializer(typeof(Level));
            using(var ms = new MemoryStream(asset.bytes))
                return (Level)xml.Deserialize(ms);
        }
    }
}