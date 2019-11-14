using System;
using UnityEngine;

namespace Yumemonogatari.Items {
    /// <summary>
    /// An armor to be worn by a character.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Items/Armor", fileName = "Armor.asset")]
    public class Armor : Item {

        /// <summary>
        /// The amount of armor provided, in HP.
        /// </summary>
        public int shield;

        /// <summary>
        /// The amount of time it takes for the armor to begin recharging
        /// </summary>
        public float rechargeDelay;

        /// <summary>
        /// The amount to restore to the player's armor in HP/sec.
        /// </summary>
        public int rechargeSpeed;
    }
}