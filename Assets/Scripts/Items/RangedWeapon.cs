using System;
using UnityEngine;

namespace Yumemonogatari.Items {
    [Serializable]
    [CreateAssetMenu(menuName = "Items/Weapons/Ranged", fileName = "RangedWeapons.asset")]
    public class RangedWeapon : Weapon {

        // don't reload if we are already reloading.
        // private bool _reloading = false;

        public enum Types {
            Gun,
            Bow
        }

        /// <summary>
        /// The type of ranged weapon.
        /// </summary>
        public Types type;

        /// <summary>
        /// The amount of ammo given after a reload
        /// </summary>
        public int ammoPerRound;

        /// <summary>
        /// The time it takes to reload ammunition.
        /// </summary>
        public float reloadSpeed;
    }
}

