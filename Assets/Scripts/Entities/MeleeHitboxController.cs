using System.Collections;
using UnityEngine;
using Yumemonogatari.Items;

namespace Yumemonogatari.Entities {
    [RequireComponent(typeof(SpriteRenderer))]
    public class MeleeHitboxController : MonoBehaviour {
        private Character _character;
        private Coroutine _anim;
        private SpriteRenderer _renderer;

        /// <summary>
        /// The current weapon represented by the controller.
        /// </summary>
        public MeleeWeapon Weapon => _character.inventory.EquippedMeleeWeapon;

        /// <summary>
        /// Whether or not the controller is activated and moving.
        /// </summary>
        public bool IsActive => _anim != null;

        private void Awake() {
            _character = gameObject.transform.parent.GetComponent<Character>();
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            Debug.Assert(_character != null);
        }

        // If we've entered a collision, stop animating, reset rotation, and deactivate.
        private void OnTriggerEnter2D(Collider2D c) {
            Interrupt();
        }

        /// <summary>
        /// The start the melee animation.
        /// </summary>
        public void Activate() {
            gameObject.SetActive(true);
            _anim = StartAnimation();
        }

        /// <summary>
        /// Interrupt the melee animation and deactivate.
        /// </summary>
        public void Interrupt() {
            if(_anim != null) {
                StopCoroutine(_anim);
                _anim = null;
            }

            var t = transform;
            var r = t.rotation;
            r.z = 0f;
            t.rotation = r;
            t.localScale = new Vector3(0.8f, 0.8f, 1);
            gameObject.SetActive(false);
        }

        // display and swing the sword
        // todo range variances with the box collider..
        private Coroutine StartAnimation() {
            var dir = _character.Direction;
            var t = gameObject.transform;
            var scale = t.localScale;
            Quaternion from;
            Quaternion target; // the Euler z-rotation that we are aiming for.

            // Make two-handed weapons wider.
            if(Weapon.type == MeleeWeapon.Types.TwoHanded)
                scale.x = 1;
            else
                scale.x = 0.8f;

            if(dir == Vector2.up) {
                _renderer.sortingOrder = Character.SortOrder - 1;
                from = Quaternion.Euler(0, 0, 30f);
                target = Quaternion.Euler(0f, 0f, -30f);
                scale.y = 1;
            }
            else if(dir == Vector2.down) {
                _renderer.sortingOrder = Character.SortOrder + 1;
                t.localScale = new Vector3(1, -1, 1);
                from = Quaternion.Euler(0, 0, -30);
                target = Quaternion.Euler(0, 0, 30f);
                scale.y *= -1; // facing down, so flip it.
            }
            else if(dir == Vector2.left) {
                _renderer.sortingOrder = Character.SortOrder - 1;
                from = Quaternion.Euler(Vector3.zero);
                target = Quaternion.Euler(0, 0, 90f);
                scale.y = 1;
            }
            else if(dir == Vector2.right) {
                _renderer.sortingOrder = Character.SortOrder + 1;
                from = Quaternion.Euler(Vector3.zero);
                target = Quaternion.Euler(0, 0, -90f);
                scale.y = 1;
            }
            else {
                Debug.LogWarning($"Unexpected character direction {dir}");
                return null;
            }

            return StartCoroutine(Rotate(from, target, Weapon.swingTime));
        }
        
        private IEnumerator Rotate(Quaternion from, Quaternion to, float duration){ 
            var elapsed = 0.0f;
            while( elapsed < duration ) {
                transform.rotation = Quaternion.Slerp(from, to, elapsed / duration );
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.rotation = to;
            Interrupt();
        }
    }
}