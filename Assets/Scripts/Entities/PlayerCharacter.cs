using System;
using UnityEngine;
using Yumemonogatari.Items;

namespace Yumemonogatari.Entities {
    public class PlayerCharacter : Character {

        private Sprite[] _sprites;
        private Camera _camera;

        protected override void Awake() {
            base.Awake();
            inventory = gameObject.AddComponent<Inventory>();
            _camera = Camera.main;
            identifier = "player";
            characterName = "Chousuke";

            Debug.Assert(_camera != null);
        }

        private void FixedUpdate() {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            var d = GetDirection(h, v);
            var sprint = Input.GetButton("Sprint");

            Move(d, sprint);
        }

        private void Update() {
            if(Input.GetButtonUp("MeleeAttack"))
                MeleeAttack();

            else if(Input.GetButtonUp("RangedAttack")) {
                Vector2 target;
                if(InputManager.InputState == InputManager.InputTypes.Keyboard)
                    target = _camera.ScreenToWorldPoint(Input.mousePosition);
                else {
                    // controller input
                    target = Direction;
                    var h = Input.GetAxis("Horizontal Aim");
                    var v = Input.GetAxis("Vertical Aim");


                    if(v > 0 || Direction == Vector2.down) {
                        target = Quaternion.AngleAxis(h * 90, Vector3.forward) * target;
                    }

                    target *= 100; // Move it a few units out
                }

                RangedAttack(target);
            }

            else if(Input.GetButtonUp("Pause") && Math.Abs(Time.timeScale) > 0.01)
                GameManager.Instance.Pause();

        }

        // Get the current direction based on input axis
        private Vector2 GetDirection(float h, float v) {
            if(Mathf.Abs(h) < 0.05f && Mathf.Abs(v) < 0.05f)
                return Vector2.zero;
            if(Mathf.Abs(h) > Mathf.Abs(v))
                return new Vector2(Mathf.Sign(h), 0);

            return new Vector2(0, Mathf.Sign(v));
        }


        public override void OnDeath() {
            throw new NotImplementedException();
        }
    }
}
