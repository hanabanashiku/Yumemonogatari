using UnityEngine;

namespace Yumemonogatari.Items {
    public class Weapon : Item {
    
        public const float CriticalMultiplier = 1.5f;
        
        /// <summary>
        /// The damage done on a normal hit.
        /// </summary>
        public int damage;
    
        /// <summary>
        /// The range of the weapon.
        /// </summary>
        public float range;
    
        /// <summary>
        /// The time between successive hits.
        /// </summary>
        public float time;
    }
}

