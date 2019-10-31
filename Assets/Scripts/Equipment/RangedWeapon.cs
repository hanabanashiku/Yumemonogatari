using System.Collections;
using UnityEngine;

namespace Yumemonogatari.Equipment {
    public class RangedWeapon : Weapon {

        // don't reload if we are already reloading.
        private bool _reloading = false;
    
        public enum RangedTypes { Gun, Bow }
    
        /// <summary>
        /// The type of ranged weapon.
        /// </summary>
        public RangedTypes Type { get; }

        /// <summary>
        /// The amount of ammo remaining in a round.
        /// </summary>
        public int remainingAmmo;
    
        /// <summary>
        /// The amount of ammo given after a reload
        /// </summary>
        public int AmmoPerRound { get; }
    
        /// <summary>
        /// The time it takes to reload ammunition.
        /// </summary>
        public float ReloadSpeed { get; }

        /// <summary>
        /// Relo
        /// </summary>
        /// <param name="inventory"></param>
        public void Reload(Inventory inventory) {
            if(inventory == null || _reloading)
                return;

            _reloading = true;
            StartCoroutine(ReloadAsync(inventory));

        }

        private IEnumerator ReloadAsync(Inventory inventory) {
            var ammo = (Type == RangedTypes.Gun) ? inventory.bullets : inventory.arrows;

            // we have no ammo
            if(ammo == 0) {
                _reloading = false;
                yield break;
            }

            yield return new WaitForSeconds(ReloadSpeed);

            // The amount of ammo we want to load
            var reloadAmount = AmmoPerRound - remainingAmmo;
        
            // we don't have enough.. give all of it.
            if(reloadAmount < ammo) 
                reloadAmount = ammo;

            ammo -= reloadAmount;
            remainingAmmo += reloadAmount;

            // Assign the new ammo amount to the character's inventory.
            if(Type == RangedTypes.Gun)
                inventory.bullets = ammo;
            else
                inventory.arrows = ammo;
            _reloading = false;
        }

        /// <summary>
        /// Fire the weapon
        /// </summary>
        /// <returns>True if there is enough ammo remaining.</returns>
        public bool Fire() {
            if(remainingAmmo <= 0)
                return false;

            remainingAmmo--;
            return true;
        }

        public RangedWeapon(string id, string name, string desc, Sprite sprite, int cost, int damage, float range, 
            float time, RangedTypes type, int ammoPerRound, float reloadSpeed, int remainingAmmo = 0) 
            : base(id, name, desc, sprite, cost, damage, range, time) {

            this.remainingAmmo = remainingAmmo;
            Type = type;
            AmmoPerRound = ammoPerRound;
            ReloadSpeed = reloadSpeed;
        }
    }
}
