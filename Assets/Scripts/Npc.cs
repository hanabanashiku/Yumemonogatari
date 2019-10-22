/// <summary>
/// An NPC not involved in combat.
/// </summary>
public class Npc : Character {
    
    // NPCs should not receive damage.
    public override void TakeDamage(int damage) {
        return;
    }

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
}
