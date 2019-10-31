namespace Yumemonogatari.Equipment {
    public class Ammunition : Item {
        public enum AmmoTypes { Arrow, Bullet }
        
        /// <summary>
        /// The type of ammo.
        /// </summary>
        public AmmoTypes Type { get; set; }
        
        /// <summary>
        /// The amount in the collectible.
        /// </summary>
        public int Amount { get; set; }

        public Ammunition(string id, string name, string desc, int cost, AmmoTypes type, int amount) :
            base(id, name, desc, cost) {
            Type = type;
            Amount = amount;
        }
    }
}