using System;
using UnityEngine;
using Yumemonogatari.Items;

namespace Yumemonogatari.Entities {
    public class AmmunitionCollectible : Collectible<Ammunition> {
        
        protected override void Collect(PlayerCharacter c) {
            if (item == null) {
                Debug.LogError("Null item picked up.");
                return;
            }
            
            switch(item.type) {
                case Ammunition.AmmoTypes.Arrow:
                    c.inventory.arrows += item.amount;
                    break;
                case Ammunition.AmmoTypes.Bullet:
                    c.inventory.bullets += item.amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(item));
            }
        }
    }
}
