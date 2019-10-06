using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour {

    private int _health;
    protected Animator Animator;
    protected SpriteRenderer Sprite;

    /// <summary>
    /// The character's current health.
    /// </summary>
    public int Health {
        get => _health;
        set {
            if(value < 0)
                _health = 0;
            else if(value > maxHealth)
                _health = maxHealth;
            else 
                _health = value;
        }
    }

    /// <summary>
    /// The character's maximum health.
    /// </summary>
    public int maxHealth;

    protected const float WalkSpeed = 3.0f;
    protected const float RunSpeed = 8.0f;

    protected virtual void Start() {
        Animator = gameObject.GetComponent<Animator>();
        Sprite = gameObject.GetComponent<SpriteRenderer>();
    }

}
