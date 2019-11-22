using System.Linq;
using UnityEngine;

namespace Yumemonogatari {
    public class InputManager : MonoBehaviour {
        public enum InputTypes { Keyboard, Controller}
        
        public enum ControllerTypes { Keyboard, XBox, PlayStation, Switch }
    
        /// <summary>
        /// The current targeted input device.
        /// </summary>
        public static InputTypes InputState { get; private set; } = InputTypes.Keyboard;
    
        private void OnGUI() {
            Debug.Log(string.Join(" ", Input.GetJoystickNames()));
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

        public ControllerTypes GetControllerType() {
            if(Input.GetJoystickNames().Length == 0)
                return ControllerTypes.Keyboard;
            
            var name = Input.GetJoystickNames()[0].ToLower();

            if(name.Contains("sony") || name.Contains("playstation"))
                return ControllerTypes.PlayStation;
            if(name.Contains("switch") || name.Contains("nintendo"))
                return ControllerTypes.Switch;
            return ControllerTypes.XBox;
        }
    }
}
