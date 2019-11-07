using System;
using UnityEngine;

public class PlayerCharacter : Character {

    private Vector2 _direction = Vector2.up;
    private Sprite[] _sprites;
    private static readonly int Direction = Animator.StringToHash("Direction");
    private static readonly int Speed = Animator.StringToHash("Speed");

    protected override void Awake() {
        base.Awake();
        inventory = gameObject.AddComponent<Inventory>();
        identifier = "player";
        characterName = "Chousuke";
    }

    private void FixedUpdate() {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var d = GetDirection(h, v);
        var speed = Input.GetButton("Sprint") ? RunSpeed : WalkSpeed;


        // changing direction
        if(d != _direction) {

            if(d == Vector2.left) {
                animator.SetInteger(Direction, 2);
                animator.SetFloat(Speed, speed); // rewrite so that these things don't happen in the same frame.
            }
            else if(d == Vector2.right) {
                animator.SetInteger(Direction, 0);
                animator.SetFloat(Speed, speed);                
            }
            else if(d == Vector2.up) {
                animator.SetInteger(Direction, 1);
                animator.SetFloat(Speed, speed);
            }

            else if(d == Vector2.down) {
                animator.SetInteger(Direction, 3);
                animator.SetFloat(Speed, speed);
            }
            else if (d == Vector2.zero) {
                animator.SetFloat(Speed, 0);
            }
            else {
                Debug.Log($"Unexpected {d}!");
            }
            _direction = d;
        }

        body.velocity = d * speed;
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        // don't reload if we are taking damage
        inventory.InterruptReload();
    }

    /// <summary>
    /// Fire a ranged weapon.
    /// </summary>
    public void Fire() {
        if(inventory.EquippedRangedWeapon == null)
            return;
        if(inventory.remainingAmmo == 0)
            return;

        inventory.remainingAmmo--;
        throw new NotImplementedException();
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
