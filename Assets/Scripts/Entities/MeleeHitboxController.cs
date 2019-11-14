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
            if(_character is null)
                Debug.LogWarning("Null character object");
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
            _anim = StartCoroutine(StartAnimation());
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
        private IEnumerator StartAnimation() {
            var dir = _character.Direction;
            var t = gameObject.transform;
            var scale = t.localScale;
            Quaternion target; // the Euler z-rotation that we are aiming for.

            // Make two-handed weapons wider.
            if(Weapon.type == MeleeWeapon.Types.TwoHanded)
                scale.x = 1;

            if(dir == Vector2.up) {
                _renderer.sortingOrder = Character.SortOrder - 1;
                t.rotation = Quaternion.Euler(0, 0, 30);
                target = Quaternion.Euler(0f, 0f, -30f);
            }
            else if(dir == Vector2.down) {
                _renderer.sortingOrder = Character.SortOrder + 1;
                t.localScale = new Vector3(1, -1, 1);
                t.rotation = Quaternion.Euler(0, 0, -30);
                target = Quaternion.Euler(0, 0, 30f);
                scale.y *= -1; // facing down, so flip it.
            }
            else if(dir == Vector2.left) {
                _renderer.sortingOrder = Character.SortOrder - 1;
                t.rotation = Quaternion.Euler(Vector3.zero);
                target = Quaternion.Euler(0, 0, 90f);

            }
            else if(dir == Vector2.right) {
                _renderer.sortingOrder = Character.SortOrder + 1;
                t.rotation = Quaternion.Euler(Vector3.zero);
                target = Quaternion.Euler(0, 0, -90f);

            }
            else {
                Debug.LogWarning($"Unexpected character direction {dir}");
                yield break;
            }

            // set the scale
            t.localScale = scale;

            var speed = (target.z - t.rotation.z) / Weapon.swingTime;

            while(t.rotation != target) {
                var step = speed * Time.deltaTime;
                t.rotation = Quaternion.RotateTowards(t.rotation, target, step);
                yield return new WaitForFixedUpdate();
            }

            // hide and reset the sword.
            Interrupt();
        }
    }
}