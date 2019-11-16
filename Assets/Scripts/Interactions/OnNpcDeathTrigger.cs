using System;

namespace Yumemonogatari.Interactions {
    [Serializable]
    public class OnNpcDeathTrigger : Trigger {
        public override TriggerTypes Type => TriggerTypes.OnNpcDeath;

        /// <summary>
        /// The identifier of the enemy that died.
        /// </summary>
        public string Identifier;

        /// <inheritdoc />
        /// <remarks>This should be checked for upon the death of an NPC</remarks>
        public override bool ConditionIsMet() => false;

    }
}