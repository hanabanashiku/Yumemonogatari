using System.Collections;
using UnityEngine;

namespace Yumemonogatari.UI {
    public class HudController : MonoBehaviour {
        public GameObject coinDisplay;
        private Coroutine _coinRoutine = null;
    
        private void Start() {
            coinDisplay.SetActive(false);
        }
    
        /// <summary>
        /// Display the coin display on the HUD for three seconds.
        /// </summary>
        public void ShowCoins() {
            if(_coinRoutine != null)
                StopCoroutine(_coinRoutine);
            
            coinDisplay.SetActive(true);
            _coinRoutine = StartCoroutine(DelayedHide(coinDisplay));
        }
    
        private static IEnumerator DelayedHide(GameObject o, float seconds = 3) {
            yield return new WaitForSeconds(seconds);
            o.SetActive(false);
        }   
    }
}
