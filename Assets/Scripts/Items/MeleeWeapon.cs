using System;
using UnityEngine;

namespace Yumemonogatari.Items {
    /// <summary>
    /// A sword or other close-range weapon.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Items/Weapons/Melee", fileName = "MeleeWeapons.asset")]
    public class MeleeWeapon : Weapon {
        public enum Types {
            OneHanded, TwoHanded
        }
    
        /// <summary>
        /// The weapon classification
        /// </summary>
        public Types type;
    
        /// <summary>
        /// The time it takes to attack with the weapon.
        /// </summary>
        public float swingTime;
    }
}

