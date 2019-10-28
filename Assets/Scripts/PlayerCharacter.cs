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
                Animator.SetInteger(Direction, 2);
                Animator.SetFloat(Speed, speed); // rewrite so that these things don't happen in the same frame.
            }
            else if(d == Vector2.right) {
                Animator.SetInteger(Direction, 0);
                Animator.SetFloat(Speed, speed);                
            }
            else if(d == Vector2.up) {
                Animator.SetInteger(Direction, 1);
                Animator.SetFloat(Speed, speed);
            }

            else if(d == Vector2.down) {
                Animator.SetInteger(Direction, 3);
                Animator.SetFloat(Speed, speed);
            }
            else if (d == Vector2.zero) {
                Animator.SetFloat(Speed, 0);
            }
            else {
                Debug.Log($"Unexpected {d}!");
            }
            _direction = d;
        }

        Body.velocity = d * speed;
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
