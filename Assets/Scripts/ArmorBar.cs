using System;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour {
    public Character character;
    private Slider _slider;

    void Awake() {
        _slider = gameObject.GetComponentInChildren<Slider>();
    }

    void Update() {
        if(character.maxShield != 0) {
            _slider.gameObject.SetActive(true);
            _slider.value = (float)character.shield / character.maxShield;
        }
        // disable the slider if there is no shield armor for the character.
        else
            _slider.gameObject.SetActive(false);

    }
}
