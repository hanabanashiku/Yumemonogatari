using System;
using UnityEngine;

public class PlayerCharacter : Character {

    private Vector2 _direction = Vector2.up;
    private Sprite[] _sprites;
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Forward = Animator.StringToHash("Forward");
    private static readonly int Backwards = Animator.StringToHash("Backwards");
    private static readonly int Stop = Animator.StringToHash("Stop");

    protected override void Start() {
        base.Start();
        _sprites = Resources.LoadAll<Sprite>("MainCharacter");
        Animator.enabled = false;
    }
    
    void Update() {
        Vector2 pos = transform.position;
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var d = GetDirection(h, v);

        // changing direction
        // todo: start animation, change sprite
        if(d != _direction) {

            if(d == Vector2.left) {
                Animator.enabled = true;
                Animator.SetTrigger(Left);
            }
            else if(d == Vector2.right) {
                Animator.enabled = true;
                Animator.SetTrigger(Right);
                
            }
            else if(d == Vector2.up) {
                Animator.enabled = true;
                Animator.SetTrigger(Forward);
            }

            else if(d == Vector2.down) {
                Animator.enabled = true;
                Animator.SetTrigger(Backwards);
            }
            else if (d == Vector2.zero) {
                Animator.SetTrigger(Stop);
                Animator.enabled = false;
                // what was the last direction?
                if(_direction == Vector2.left)
                    Sprite.sprite = _sprites[8];
                else if(_direction == Vector2.right)
                    Sprite.sprite = _sprites[11];
                else if(_direction == Vector2.up)
                    Sprite.sprite = _sprites[2];
                else if(_direction == Vector2.down)
                    Sprite.sprite = _sprites[5];
            }
            else {
                Debug.Log($"Unexpected {d}!");
            }
            _direction = d;
        }

        var speed = Input.GetButton("Sprint") ? RunSpeed : WalkSpeed;
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
