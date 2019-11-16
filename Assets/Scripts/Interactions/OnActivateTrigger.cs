using System;

namespace Yumemonogatari.Interactions {
    [Serializable]
    public class OnActivateTrigger : Trigger {
        public override TriggerTypes Type => TriggerTypes.OnActivate;

        /// <summary>
        /// The object or NPC identifier.
        /// </summary>
        public string Identifier;

        /// <inheritdoc />
        /// <remarks>This should be checked for upon activating an NPC</remarks>
        public override bool ConditionIsMet() => false;

    }
}