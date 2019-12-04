using System;
using UnityEngine;
using UnityEngine.UI;
using Yumemonogatari.Entities;
using Yumemonogatari.Items;

namespace Yumemonogatari.UI {
    public class WeaponDisplayController : MonoBehaviour {
    
        public Inventory inventory;
        public Image meleeImage;
        public Image rangedImage;
        public Text currentAmmo;
        public Text totalAmmo;
        public GameObject loadingCircle;
    
        private void Start() {
            var player = FindObjectOfType<PlayerCharacter>();
            inventory = player.inventory;
            Debug.Assert(inventory != null);
        }
    
        private void OnGUI() {
            // check melee weapon for update
            if(meleeImage.sprite != null && inventory.EquippedMeleeWeapon == null)
                Clear(meleeImage);
            else if(inventory.EquippedMeleeWeapon != null && inventory.EquippedMeleeWeapon.sprite != meleeImage.sprite)
                SetImage(meleeImage, inventory.EquippedMeleeWeapon.sprite);
            
            // check ranged weapon for update
            if(rangedImage.sprite != null && inventory.EquippedRangedWeapon == null) {
                Clear(rangedImage);
                currentAmmo.gameObject.SetActive(false);
                totalAmmo.gameObject.SetActive(false);
            }
            else if(inventory.EquippedRangedWeapon != null && inventory.EquippedRangedWeapon.sprite != rangedImage.sprite) {
                SetImage(rangedImage, inventory.EquippedRangedWeapon.sprite);
                currentAmmo.gameObject.SetActive(true);
                totalAmmo.gameObject.SetActive(true);
            }
            
            // set ammo display
            if(inventory.EquippedRangedWeapon != null) {
                currentAmmo.text = $"{inventory.remainingAmmo}";
                totalAmmo.text = inventory.EquippedRangedWeapon.type == RangedWeapon.Types.Gun
                    ? $"\\ {inventory.bullets}"
                    : $"\\ {inventory.arrows}";
            }
    
            else {
                currentAmmo.gameObject.SetActive(false);
                totalAmmo.gameObject.SetActive(false);
            }
            
            if(inventory.Reloading)
                loadingCircle.SetActive(true);
            else
                loadingCircle.SetActive(false);
        }
    
        private static void Clear(Image image) {
            image.sprite = null;
            image.color = new Color(255, 255, 255, 0);
        }
    
        private static void SetImage(Image image, Sprite sprite) {
            image.sprite = sprite;
            image.color = Color.white;
        }
    }
}
