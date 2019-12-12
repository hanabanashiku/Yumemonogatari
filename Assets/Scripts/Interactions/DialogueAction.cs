using System;
using UnityEngine;
using Yumemonogatari.UI;

namespace Yumemonogatari.Interactions {
    /// <summary>
    /// An action to display a line of dialogue on screen.
    /// </summary>
    [Serializable]
    public class DialogueAction : ScriptedAction {
        public override ActionTypes Type => ActionTypes.Dialogue;

        /// <summary>
        /// The identifier of the speaker.
        /// </summary>
        public string identifier;

        /// <summary>
        /// The name of the speaker
        /// </summary>
        /// <remarks>Will only be used if Identifier is null or empty.</remarks>
        public string speakerName;

        /// <summary>
        /// The text to be spoken
        /// </summary>
        public string dialogue;

        /// <summary>
        /// The font to display the text with.
        /// </summary>
        public FontData fontDisplay;

        /// <summary>
        /// If true, the UI will wait for a key press before completing the action.
        /// </summary>
        public bool waitForInput;
        
        public override void Perform() {
            Debug.Assert(DialogueBoxController.Instance != null);
            Debug.Log("Performing");
            string speaker;
            if(string.IsNullOrEmpty(identifier))
                speaker = speakerName;
            else {
                var npc = GameManager.FindNpc(identifier);
                if(npc is null) {
                    Debug.LogWarning($"Could not find NPC {identifier}");
                    speaker = identifier;
                }
                else speaker = npc.characterName;
            }
            DialogueBoxController.Instance.UpdateDialogue(speaker, this);
        }

        public static implicit operator string(DialogueAction a) {
            return a.dialogue;
        }

        [Serializable]
        public class FontData {
            public int fontSize;
            public FontStyle fontStyle;

            public FontData() {
                fontSize = 14;
                fontStyle = FontStyle.Normal;
            }
        }
    }
}