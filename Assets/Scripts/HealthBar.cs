using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    
    public Character character;
    private Slider _slider;

    void Awake() {
        _slider = gameObject.GetComponentInChildren<Slider>();
    }

    void Update() {
       _slider.value = (float)character.health / character.maxHealth;
    }
}
