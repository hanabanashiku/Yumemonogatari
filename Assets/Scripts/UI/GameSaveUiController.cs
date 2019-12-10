using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Yumemonogatari.UI {
    public abstract class GameSaveUiController : MonoBehaviour {
        public Transform entries;

        protected List<GameSaveEntryDisplay> Displays;
        protected int Selected;
        private GameObject _entryPrefab;
        private GameObject _previousSelection;

        protected void Awake() {
            _entryPrefab = AssetBundles.Ui.LoadAsset<GameObject>("Save Item");
            Displays = new List<GameSaveEntryDisplay>();
            _previousSelection = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(gameObject);
            Populate();

            if(Displays.Count > 0) {
                Selected = 0;
                Displays[0].Select();
            }
        }

        protected void Update() {
            if(Input.GetButtonUp("Cancel")) {
                Destroy(gameObject);
                EventSystem.current.SetSelectedGameObject(_previousSelection);
            }
            else if(Input.GetButtonUp("Submit"))
                Submit();
            else if(Input.GetAxis("Vertical") > 0)
                MoveUp();
            else if(Input.GetAxis("Vertical") < 0)
                    MoveDown();
            
        }

        protected void Populate() {
            var saves = Directory.GetFiles(Application.persistentDataPath, "*.sav",
                SearchOption.TopDirectoryOnly).Select(GameSave.Deserialize).OrderByDescending(x => x.number);

            foreach(var s in saves) {
                var obj = Instantiate(_entryPrefab, entries, false);
                var display = obj.GetComponent<GameSaveEntryDisplay>();
                display.Save = s;
                Displays.Add(display);
            }
        }

        protected virtual void MoveUp() {
            if(Displays.Count == 0 || Selected == 0)
                return;
            Displays[Selected].Deselect();
            Selected--;
            Displays[Selected].Select();
        }

        protected virtual void MoveDown() {
            if(Displays.Count == 0 || Selected == Displays.Count - 1)
                return;
            Displays[Selected].Deselect();
            Selected++;
            Displays[Selected].Select();
        }

        protected abstract void Submit();
    }
}