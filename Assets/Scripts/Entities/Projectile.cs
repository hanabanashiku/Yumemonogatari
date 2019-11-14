using UnityEngine;
using Yumemonogatari.Items;

namespace Yumemonogatari.Entities {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour {

        public RangedWeapon weapon;
        public Vector2 target;

        public const float ProjectileSpeed = 15f;

        // todo factor in range
        private void Start() {
            // Rotate towards target.
            transform.LookAt(target);

            // give it a velocity
            var body = GetComponent<Rigidbody2D>();
            body.AddRelativeForce(Vector2.up * ProjectileSpeed, ForceMode2D.Force);
        }

        private void OnTriggerEnter2D(Collider2D c) {
            Destroy(gameObject);
        }

        // This is if it hits an environment
        private void OnCollisionEnter2D(Collision2D c) {
            // ignore character colliders.
            if(c.otherRigidbody.gameObject.GetComponent<Character>() != null)
                return;

            // chance of projectile landing on ground
            if(weapon.type == RangedWeapon.Types.Bow && Random.value < 0.4f) {
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
}