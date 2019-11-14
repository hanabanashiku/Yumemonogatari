using System;
using UnityEngine;
using Yumemonogatari.Entities;

namespace Yumemonogatari.Items {
    /// <summary>
    /// An item that restores health or shield to the consumer.
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "Items/Consumables", fileName = "Consumables.asset")]
    public class Consumable : Item {

        /// <summary>
        /// The amount of health to restore.
        /// </summary>
        public int health;

        /// <summary>
        /// The amount of shield to restore.
        /// </summary>
        public int shield;

        /// <summary>
        /// Perform the effects of the item on the target.
        /// </summary>
        /// <param name="target">The target.</param>
        public void Consume(Character target) {
            target.shield = Math.Min(target.shield + shield, target.MaxShield);
            target.health = Math.Min(target.health + health, target.maxHealth);
        }
    }
}
