using System;
using UnityEngine;

public class PlayerCharacter : Character {

    private Vector2 _direction = Vector2.up;
    private Sprite[] _sprites;
    private static readonly int Direction = Animator.StringToHash("Direction");
    private static readonly int Speed = Animator.StringToHash("Speed");

    protected override void Start() {
        base.Start();
        Animator.enabled = false;
    }
    
    void Update() {
        Vector2 pos = transform.position;
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var d = GetDirection(h, v);
        var speed = Input.GetButton("Sprint") ? RunSpeed : WalkSpeed;


        // changing direction
        // todo: start animation, change sprite
        if(d != _direction) {

            if(d == Vector2.left) {
                Animator.enabled = true;
                Animator.SetInteger(Direction, 2);
                Animator.SetFloat(Speed, speed);
            }
            else if(d == Vector2.right) {
                Animator.enabled = true;
                Animator.SetInteger(Direction, 0);
                Animator.SetFloat(Speed, speed);
                
            }
            else if(d == Vector2.up) {
                Animator.enabled = true;
                Animator.SetInteger(Direction, 1);
                Animator.SetFloat(Speed, speed);
            }

            else if(d == Vector2.down) {
                Animator.enabled = true;
                Animator.SetInteger(Direction, 3);
                Animator.SetFloat(Speed, speed);
            }
            else if (d == Vector2.zero) {
                Animator.SetFloat(Speed, 0);
                Animator.enabled = false;
            }
            else {
                Debug.Log($"Unexpected {d}!");
            }
            _direction = d;
        }

        pos += speed * d * Time.deltaTime;
        transform.position = pos;
    }
    
    // Get the current direction based on input axis
    protected Vector2 GetDirection(float h, float v) {
        if(Mathf.Abs(h) < 0.05f && Mathf.Abs(v) < 0.05f)
            return Vector2.zero;
        if(Mathf.Abs(h) > Mathf.Abs(v)) 
            return new Vector2(Mathf.Sign(h), 0);
       
        return new Vector2(0, Mathf.Sign(v));
    }
}
