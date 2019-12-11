using UnityEngine;
using UnityEngine.UI;

namespace Yumemonogatari.UI {
    /// <summary>
    /// Displays an item on a grid
    /// </summary>
    public class ItemDisplayController : MonoBehaviour {
        public Item item;
        public SpriteRenderer border;
        public Text quantityDisplay;
        public SpriteRenderer itemImage;
        public Image equippedIcon;
    
        private static Sprite _borderNormal;
        private static Sprite _borderSelected; 
        private static Sprite _equippedStar; // the sprite that represents the item being equipped
    
        private void Awake() {
            if(_borderNormal is null)
                _borderNormal = AssetBundles.Ui.LoadAsset<Sprite>("border-blue.");
            if(_borderSelected is null)
                _borderSelected = AssetBundles.Ui.LoadAsset<Sprite>("border-select");
            if(_equippedStar is null)
                _equippedStar = AssetBundles.Ui.LoadAsset<Sprite>("equipped");
            
            border = gameObject.AddComponent<SpriteRenderer>();
            Deselect(); // set the border image as normal
            border.drawMode = SpriteDrawMode.Simple;
            border.sortingOrder = 50;
            
            var obj = new GameObject("Quantity Display");
            obj.transform.localPosition = new Vector2(15f, -10f);
            quantityDisplay = obj.AddComponent<Text>();
            quantityDisplay.fontSize = 18;
            quantityDisplay.color = Color.white;
            quantityDisplay.text = "0";
            transform.localScale = new Vector3(50, 50, 1);
        }
    
        private void Start() {
            // display the item image
            var obj = new GameObject("Item Image");
            itemImage = obj.AddComponent<SpriteRenderer>();
            itemImage.sprite = item.sprite;
            obj.transform.SetParent(transform, false);
        }
    
        public void Select() {
            border.sprite = _borderSelected;
        }
    
        public void Deselect() {
            border.sprite = _borderNormal;
        }
    
        /// <summary>
        /// Add an equipped icon.
        /// </summary>
        public void Equip() {
            if(equippedIcon != null)
                return;
    
            var obj = new GameObject("Equipped Icon");
            var t = obj.transform;
            t.localScale = new Vector2(0.5f, 0.5f);
            t.localPosition = new Vector2(-15f, 15f);
            equippedIcon = obj.AddComponent<Image>();
            equippedIcon.sprite = _equippedStar;
            //equippedIcon.sortingOrder = -1;
        }
    
        /// <summary>
        /// Remove the equipped icon.
        /// </summary>
        public void Dequip() {
            if(equippedIcon is null)
                return;
            
            Destroy(equippedIcon);
        }
    
        public void SetQuantity(int q) {
            quantityDisplay.text = $"{q}";
            quantityDisplay.gameObject.SetActive(q > 1);
        }
    
        /// <summary>
        /// Delete this item from the menu.
        /// </summary>
        public void Delete() {
            Destroy(gameObject);
        }
    
        public static implicit operator Item(ItemDisplayController c) {
            return c.item;
        }
    }
}