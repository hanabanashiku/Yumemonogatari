using System.Collections.Generic;
using UnityEngine;

namespace Yumemonogatari.Equipment {
    /// <summary>
    /// The player's inventory and equipped items.
    /// </summary>
    public class Inventory : MonoBehaviour {
        /// <summary>
        /// The player's items
        /// </summary>
        public List<Item> items;

        /// <summary>
        /// The amount of currency held by the player.
        /// </summary>
        public int mon;

        /// <summary>
        /// The number of arrows the player has.
        /// </summary>
        public int arrows;

        /// <summary>
        /// The number of bullets the player has.
        /// </summary>
        public int bullets;

        private Armor _equippedArmor;

        /// <summary>
        /// The armor that is currently equipped.
        /// </summary>
        public Armor EquippedArmor => _equippedArmor;

        private RangedWeapon _ranged;

        /// <summary>
        /// The bow or gun that is currently equipped.
        /// </summary>
        public RangedWeapon EquippedRangedWeapon => _ranged;

        private MeleeWeapon _melee;

        /// <summary>
        /// The sword that is currently equipped.
        /// </summary>
        public MeleeWeapon EquippedMeleeWeapon => _melee;

        /// <summary>
        /// Equip a melee weapon. Pass null to remove.
        /// </summary>
        /// <param name="weapon">The weapon to equip.</param>
        public void EquipWeapon(MeleeWeapon weapon) {
            if(weapon == null) {
                _melee = null;
                return;
            }

            if(!items.Contains(weapon)) {
                Debug.Log($"The user's inventory does not contain {weapon.Name}");
                return;
            }

            _melee = weapon;
        }

        /// <summary>
        /// Equip a ranged weapon. Pass null to remove.
        /// </summary>
        /// <param name="weapon">The weapon to equip.</param>
        public void EquipWeapon(RangedWeapon weapon) {
            if(weapon == null) {
                _melee = null;
                return;
            }

            if(!items.Contains(weapon)) {
                Debug.Log($"The user's inventory does not contain {weapon.Name}");
                return;
            }

            _ranged = weapon;
        }

        /// <summary>
        /// Equip an armor. Pass null to remove.
        /// </summary>
        /// <param name="armor">The armor to equip.</param>
        public void EquipArmor(Armor armor) {
            if(armor == null) {
                _equippedArmor = null;
                return;
            }

            if(!items.Contains(armor)) {
                Debug.Log($"The user's inventory does not contain {armor.Name}");
                return;
            }

            _equippedArmor = armor;
        }

        public Inventory() {
            items = new List<Item>();
        }
    }
}
