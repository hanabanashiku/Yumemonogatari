using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Yumemonogatari.Entities {
    public class Collectible<TItem> : MonoBehaviour where TItem : Item {
        [CanBeNull] public TItem item;

        protected void OnTriggerEnter2D(Collider2D c) {
            var character = c.gameObject.GetComponent<PlayerCharacter>();
            if(character == null)
                return;
            Collect(character);
            Destroy(gameObject);

        }

        protected virtual void Collect(PlayerCharacter c) {
            c.inventory.Add(item);
        }

        /// <summary>
        /// Create a collectible on the map for pickup.
        /// </summary>
        /// <param name="box">The bounding box of the collider it hit.</param>
        /// <returns>The collectible object.</returns>
        public GameObject Instantiate(Bounds box) {
            var point = Vector3.zero;
            point.x = box.center.x;
            point.y = box.center.y - box.size.y * 0.5f;
            return Instantiate(point);
        }

        /// <summary>
        /// Create a collectible on the map for pickup.
        /// </summary>
        /// <param name="location">The point to center the collectible on.</param>
        /// <returns>The collectible object.</returns>
        /// <exception cref="NullReferenceException">If the attached item is null.</exception>
        public GameObject Instantiate(Vector2 location) {
            if(item == null) {
                throw new NullReferenceException("Tried to instantiate a null item.");
            }

            var c = Instantiate(new GameObject("Collectible"));
            c.transform.position = location;
            var r = c.AddComponent<SpriteRenderer>();
            r.sprite = item.sprite;
            Attach(c);
            var box = c.AddComponent<BoxCollider2D>();
            box.size = r.size;
            return c;
        }

        protected virtual void Attach(GameObject c) {
            var collectible = c.AddComponent(GetType()) as Collectible<TItem>;
            if(collectible == null) throw new InvalidCastException();
            collectible.item = item;
        }
    }
}