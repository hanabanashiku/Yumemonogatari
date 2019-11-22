using UnityEngine;
using Yumemonogatari.Interactions;
using Yumemonogatari.Items;

namespace Yumemonogatari.Entities {
    public class Enemy : Character {
        public override void OnDeath() {
            InteractionManager.Instance.NpcDeath(identifier); // tell the game there was a death
            
            // instantiate items to drop
            var c = GetComponent<BoxCollider2D>();
            var box = c.bounds;
            foreach(var i in inventory) {
                // instantiate all health items etc, and only weapons and ammo at a 30% chance.
                if((i.GetType() == typeof(MeleeWeapon) || i.GetType() == typeof(RangedWeapon) ||
                    i.GetType() == typeof(Armor)) && !(Random.value < 3)) 
                    continue;
                
                var col = gameObject.AddComponent<Collectible<Item>>();
                col.item = i;
                col.Instantiate(box);
            }
            
            // delete the character
            Destroy(gameObject);
        }
    }
}