using Yumemonogatari.Interactions;
using Yumemonogatari.Items;

namespace Yumemonogatari.Entities {
    /// <summary>
    /// An NPC not involved in combat.
    /// </summary>
    public class Npc : Character, IInteractable {

        // NPCs should not receive damage.
        public override void TakeDamage(int damage) {}

        public override void OnDeath() {
            // destroy the game object and move on.
            // This function probably shouldn't even be called.
            Destroy(gameObject);
        }

        public Npc() {
            inventory = gameObject.AddComponent<Inventory>();
            health = 1;
            maxHealth = 1;
        }

        public void Interact() {
            InteractionManager.Instance.ObjectActivated(identifier);
        }
    }
}
