using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Yumemonogatari.Interactions;

namespace Yumemonogatari.UI {
    /// <summary>
    /// The controller for the UI dialogue box.
    /// </summary>
    public class DialogueBoxController : MonoBehaviour {
        public Text speaker;
        public Text dialogue;

        // true if we are displaying text and waiting for the next prompt.
        private bool _waitingForInput;
        // the routine that is printing the text
        [CanBeNull]
        private Coroutine _printRoutine;

        private event InteractionManager.OnActionCompleted ActionCompleted;

        private void Start() {
            ActionCompleted += InteractionManager.Instance.ActionCompleted;
        }
        
        private void OnEnable() {
            Time.timeScale = 0;
        }

        private void OnDisable() {
            Time.timeScale = 1;
        }

        private void Update() {
            if(_waitingForInput && Input.GetButtonUp("Submit")) 
                Complete();
        }

        /// <summary>
        /// Start printing dialogue text to the screen.
        /// </summary>
        /// <param name="speakerName">The name of the speaker.</param>
        /// <param name="text">The dialogue to print.</param>
        public void UpdateDialogue(string speakerName, DialogueAction text) {
            gameObject.SetActive(true);
            var font = text.fontDisplay;
            dialogue.fontSize = font.fontSize;
            dialogue.fontStyle = font.fontStyle;
            speaker.text = speakerName;
            _printRoutine = StartCoroutine(PrintTextAsync(text, text.waitForInput));
        }

        private IEnumerator PrintTextAsync(string text, bool input) {
            var delay = 1f / (int)SettingsManager.Instance.textSpeed;
            
            foreach(var c in text) {
                dialogue.text += c;
                if(!char.IsWhiteSpace(c))
                    yield return new WaitForSeconds(delay);
            }

            if(input)
                _waitingForInput = true;
            else 
                Complete();
        }

        private void Complete() {
            ClearText();
            _waitingForInput = false;
            ActionCompleted?.Invoke();
            gameObject.SetActive(false);
        }

        private void ClearText() {
            speaker.text = string.Empty;
            dialogue.text = string.Empty;
        }
    }
}