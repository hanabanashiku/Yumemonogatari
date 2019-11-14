using UnityEngine;
using UnityEngine.UI;
using Yumemonogatari.Entities;

namespace Yumemonogatari.UI {
    public class HealthBar : MonoBehaviour {
        
        public Character character;
        private Slider _slider;
    
        private void Awake() {
            _slider = gameObject.GetComponentInChildren<Slider>();
            if(character is null)
                character = FindObjectOfType<PlayerCharacter>();
        }
    
        private void Update() {
           _slider.value = (float)character.health / character.maxHealth;
        }
    }
}

