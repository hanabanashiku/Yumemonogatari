using UnityEngine;
using Yumemonogatari.Items;
using Yumemonogatari.UI;

namespace Yumemonogatari.Entities {
    [RequireComponent(typeof(Store))]
    public class Shopkeeper : Npc {
        private Store _store;
        protected override void Awake() {
            base.Awake();
            _store = GetComponent<Store>();
        }
        
        public override void Interact() {
            var obj = Instantiate(AssetBundles.Ui.LoadAsset<GameObject>("Store Menu"));
            var controller = obj.GetComponentInChildren<StoreDisplayController>();
            controller.store = _store;
        }
    }
}