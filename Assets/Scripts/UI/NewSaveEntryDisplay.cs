using System;

namespace Yumemonogatari.UI {
    public class NewSaveEntryDisplay : GameSaveEntryDisplay {
        public override GameSave Save {
            get => null;
            set => throw new NotSupportedException();
        }
    }
}