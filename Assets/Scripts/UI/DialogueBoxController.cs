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
        public static DialogueBoxController Instance;
        
        public Text speaker;
        public Text dialogue;

        // true if we are displaying text and waiting for the next prompt.
        private bool _waitingForInput;
        // the routine that is printing the text
        [CanBeNull]
        private Coroutine _printRoutine;

        private event InteractionManager.OnActionCompleted ActionCompleted;

        private void Awake() {
            ActionCompleted += InteractionManager.Instance.ActionCompleted;
        }

        public DialogueBoxController() {
            Instance = this;
        }
        
        private void OnEnable() {
            Time.timeScale = 0;
        }

        private void OnDisable() {
            Time.timeScale = 1;
        }

        private void Update() {
            if(_waitingForInput && Input.GetButtonUp("Submit")) {
                Debug.Log("Submit");
                _waitingForInput = false;
            }
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

            // text speed is 0 (fast)
            if(float.IsInfinity(delay)) {
                dialogue.text = text;
            }
            else {
                foreach(var c in text) {
                    dialogue.text += c;
                    if(!char.IsWhiteSpace(c))
                        yield return new WaitForSeconds(delay);
                }
            }

            if(input)
                _waitingForInput = true;

            yield return new WaitWhile(() => _waitingForInput);
            
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