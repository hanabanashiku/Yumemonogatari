using UnityEngine;
using UnityEngine.UI;
using Yumemonogatari.Entities;
using Yumemonogatari.Items;

namespace Yumemonogatari.UI {
    public class AmmoDisplayController : MonoBehaviour {
        public Text arrowText;
        public Text bulletText;
        
        private Inventory _inventory;
    
        private void Start() {
            var player = FindObjectOfType<PlayerCharacter>();
            if(player == null) {
                Debug.Log("Could not find player character.");
                return;
            }
    
            _inventory = player.inventory;
        }
    
        private void Update() {
            arrowText.text = $"{_inventory.arrows}";
            bulletText.text = $"{_inventory.bullets}";
        }
    }
}
