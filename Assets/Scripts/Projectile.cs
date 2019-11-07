using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public RangedWeapon weapon;

    private void OnCollisionEnter2D(Collision2D c) {
        // chance of projectile landing on ground
        if (weapon.type == RangedWeapon.Types.Bow && Random.value < 0.4f) {
            // get the point on the collider (assumed to be a Box2DCollider) that is on the middle of the bottom edge.
            var col = c.collider;
            var bounds = col.bounds;
            var point = Vector3.zero;
            point.x = bounds.center.x;
            point.y = bounds.center.y - bounds.size.y * 0.5f;
            
            var collectible = gameObject.AddComponent<AmmunitionCollectible>();
            collectible.item = Resources.Load<Ammunition>("Items/arrow");
            collectible.Instantiate(point);
        }
        
        Destroy(gameObject);
    }
}