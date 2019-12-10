namespace Yumemonogatari.UI {
    public class GameLoadDisplayController : GameSaveUiController {
        protected override void Submit() {
            if(Displays.Count == 0)
                return;
            Displays[Selected].Save.Load();
        }
    }
}