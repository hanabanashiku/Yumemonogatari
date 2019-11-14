using UnityEngine;

namespace Yumemonogatari {
    public class InputManager : MonoBehaviour {
        public enum InputTypes { Keyboard, Controller}
    
        /// <summary>
        /// The current targeted input device.
        /// </summary>
        public static InputTypes InputState { get; private set; } = InputTypes.Keyboard;
    
        private void OnGUI() {
            var e = Event.current;
    
            if(e.isMouse || e.isScrollWheel)
                InputState = InputTypes.Keyboard;
    
            else if(e.isKey) {
                var code = e.keyCode;
                
                // check the joystick button range
                if((int)e.keyCode >= 350 && (int)e.keyCode <= 350 + 20 * 8)
                    InputState = InputTypes.Controller;
    
                else
                    InputState = InputTypes.Keyboard;
            }
        }
    }
}
