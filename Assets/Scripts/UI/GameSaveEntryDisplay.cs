using UnityEngine;
using UnityEngine.UI;

namespace Yumemonogatari.UI {
    public class GameSaveEntryDisplay : MonoBehaviour {
        private static Sprite _border;
        private static Sprite _selected;
        private GameSave _save;

        public Text number;
        public Text scene;
        public Text time;
        public Image border;

        public GameSave Save {
            get => _save;
            set {
                _save = value;
                number.text = _save.number.ToString("D2");
                scene.text = _save.currentScene;
                time.text = _save.time.ToString("dd MMM yyyy HH:mm:ss");
            }
        }

        private void Awake() {
            if(_border is null)
                _border = AssetBundles.Ui.LoadAsset<Sprite>("border-blue");
            if(_selected is null)
                _selected = AssetBundles.Ui.LoadAsset<Sprite>("border-selected");
        }

        public void Select() {
            border.sprite = _selected;
        }

        public void Deselect() {
            border.sprite = _border;
        }
    }
}