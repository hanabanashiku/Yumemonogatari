using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour {
    public Character character;
    private Slider _slider;

    private void Awake() {
        _slider = gameObject.GetComponentInChildren<Slider>();
        if(character is null)
            character = FindObjectOfType<PlayerCharacter>();
    }

    private void Update() {
        if(character.MaxShield != 0) {
            _slider.gameObject.SetActive(true);
            _slider.value = (float)character.shield / character.MaxShield;
        }
        // disable the slider if there is no shield armor for the character.
        else
            _slider.gameObject.SetActive(false);

    }
}
