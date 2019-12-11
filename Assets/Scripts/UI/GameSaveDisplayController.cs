using System.Linq;

namespace Yumemonogatari.UI {
    public class GameSaveDisplayController : GameSaveUiController {

        protected override void Awake() {
            base.Awake();
            Displays.Insert(0, gameObject.GetComponentInChildren<NewSaveEntryDisplay>());
            // We've added a new one, so select that one instead
            if(Displays.Count > 1)
                Displays[1].Deselect();
            Displays[0].Select();
        }
        
        protected override void Submit() {
            var selected = Displays[Selected];
            int number;
            if(selected.GetType() == typeof(NewSaveEntryDisplay)) {
                if(Displays.Count == 1)
                    number = 1;
                else
                    number = -1; // make the gameManager figure it out
            }
            else
                number = selected.Save.number;
            
            GameManager.SaveGame(number);
            Destroy(gameObject);
        }
    }
}