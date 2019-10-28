using System.Collections;
using UnityEngine;

public class HudController : MonoBehaviour {
    private GameObject _coinDisplay;
    private Coroutine _coinRoutine = null;

    private void Start() {
        _coinDisplay = GameObject.Find("Coin Display");
        _coinDisplay.SetActive(false);
    }

    /// <summary>
    /// Display the coin display on the HUD for three seconds.
    /// </summary>
    public void ShowCoins() {
        if(_coinRoutine != null)
            StopCoroutine(_coinRoutine);
        
        _coinDisplay.SetActive(true);
        _coinRoutine = StartCoroutine(DelayedHide(_coinDisplay));
    }

    private static IEnumerator DelayedHide(GameObject o, float seconds = 3) {
        yield return new WaitForSeconds(seconds);
        o.SetActive(false);
    }
        
}