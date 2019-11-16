using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yumemonogatari.Interactions {
    [Serializable]
    public class OnPlayerEnterTrigger : Trigger {
        public override TriggerTypes Type => TriggerTypes.OnPlayerEnter;

        /// <summary>
        /// The name of the scene
        /// </summary>
        public string SceneName;

        /// <summary>
        /// The bounding box of the area of the scene.
        /// </summary>
        public Bounds Boundary;

        public override bool ConditionIsMet() {
            if(!SceneManager.GetActiveScene().name.Equals(SceneName))
                return false;
            
            var player = GameObject.FindWithTag("Player");
            return Boundary.Contains(player.transform.position);
        }
    }
}