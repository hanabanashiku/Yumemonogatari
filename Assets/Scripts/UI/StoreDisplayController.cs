using System.Linq;
using UnityEngine;
using Yumemonogatari.Items;

namespace Yumemonogatari.UI {
    public class StoreDisplayController : InventoryDisplayController {
        public Store store;
        public RectTransform shopNavPointer;
        public Transform buyLink;
        public Transform sellLink;
        
        public enum StorePages {Buy, Sell}

        public StorePages currentPage;

        private void Awake() {
            Time.timeScale = 0;
            GameManager.Instance.hud.SetActive(false);
        }

        private void OnDestroy() {
            Time.timeScale = 1;
            GameManager.Instance.hud.SetActive(true);
        }

        protected override void Update() {
            if(Input.GetButton("Menu Left") || Input.GetButton("Menu Right")) {
                currentPage = currentPage == StorePages.Buy ? StorePages.Sell : StorePages.Buy;
                Cache();
                Clear();
                Populate();

                shopNavPointer.SetParent(currentPage == StorePages.Buy ? buyLink : sellLink);
                shopNavPointer.anchorMin = new Vector2(0.5f, 0);
                shopNavPointer.anchorMax = new Vector2(0.5f, 0);
            }
            else if(Input.GetButtonUp("Pause") || Input.GetButtonUp("Cancel"))
                Destroy(gameObject);
            
            base.Update();
        }

        protected override void Cache() {
            Lists.Clear();

            if(currentPage == StorePages.Buy) {
                Lists[Pages.Melee] = store[typeof(MeleeWeapon)].Select(x => LoadItem(x)).ToList();
                Lists[Pages.Ranged] = store[typeof(RangedWeapon)].Select(x => LoadItem(x)).ToList();
                Lists[Pages.Armor] = store[typeof(Armor)].Select(x => LoadItem(x)).ToList();
                Lists[Pages.Consumable] = store[typeof(Consumable)].Select(x => LoadItem(x)).ToList();
            }
            else // pull from the inventory.
                base.Cache();
        }

        protected override void ActivateItem(ItemDisplayController idc) {
            var item = (Item)idc;
            
            if(currentPage == StorePages.Buy)
                store.Buy(inventory, item, 1);
            else
                store.Sell(inventory, item, 1);
        }
    }
}