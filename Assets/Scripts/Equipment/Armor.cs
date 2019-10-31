using UnityEngine;

namespace Yumemonogatari.Equipment {
    /// <summary>
    /// An armor to be worn by a character.
    /// </summary>
    public class Armor : Item {
    
        /// <summary>
        /// The amount of armor provided, in HP.
        /// </summary>
        public int Shield { get; }
    
        /// <summary>
        /// The amount of time it takes for the armor to begin recharging
        /// </summary>
        public float RechargeDelay { get; }
    
        /// <summary>
        /// The amount to restore to the player's armor in HP/sec.
        /// </summary>
        public int RechargeSpeed { get; }

        public Armor(string id, string name, string desc, Sprite sprite, int cost, int shield, float rechargeDelay, 
            int rechargeSpeed) : 
            base(id, name, desc, cost) {
        
            Shield = shield;
            RechargeDelay = rechargeDelay;
            RechargeSpeed = rechargeSpeed;
        }
    }
}
