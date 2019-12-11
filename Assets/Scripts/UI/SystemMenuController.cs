using UnityEngine;
using UnityEngine.EventSystems;


namespace Yumemonogatari.UI {
    public class SystemMenuController : MonoBehaviour {
        
        public GameObject firstButton;

        private void OnEnable() {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        public void TriggerQuit() {
            var prefab = AssetBundles.Ui.LoadAsset<GameObject>("Quit Message");
            Instantiate(prefab);
        }
    }
}