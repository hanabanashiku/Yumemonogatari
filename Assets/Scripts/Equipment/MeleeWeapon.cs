using UnityEngine;

namespace Yumemonogatari.Equipment {
    /// <summary>
    /// A sword or other close-range weapon.
    /// </summary>
    public class MeleeWeapon : Weapon {
        public enum MeleeTypes {
            OneHanded, TwoHanded
        }

        /// <summary>
        /// The weapon classification
        /// </summary>
        public MeleeTypes Type { get; }

        public MeleeWeapon(string id, string name, string desc, Sprite sprite, int cost, int damage, float range, 
            float time, MeleeTypes type) 
            : base(id, name, desc, sprite, cost, damage, range, time) {
        
            Type = type;
        }
    }
}
