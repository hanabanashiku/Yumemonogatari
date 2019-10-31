using System;
using UnityEngine;

namespace Yumemonogatari.Equipment {
    /// <summary>
    /// An item that restores health or shield to the consumer.
    /// </summary>
    public class Consumable : Item {
    
        /// <summary>
        /// The amount of health to restore.
        /// </summary>
        public int Health { get; }
    
        /// <summary>
        /// The amount of shield to restore.
        /// </summary>
        public int Shield { get; }

        /// <summary>
        /// Perform the effects of the item on the target.
        /// </summary>
        /// <param name="target">The target.</param>
        public void Consume(Character target) {
            target.shield = Math.Min(target.shield + Shield, target.MaxShield);
            target.health = Math.Min(target.health + Health, target.maxHealth);
        }

        public Consumable(string id, string name, string desc, Sprite sprite, int cost, int health, int shield) 
            : base(id, name, desc, cost) {
            Health = health;
            Shield = shield;
        }
    }
}
