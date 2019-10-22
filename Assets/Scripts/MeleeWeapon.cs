using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A sword or other close-range weapon.
/// </summary>
public class MeleeWeapon : Weapon {
    public enum Types {
        OneHanded, TwoHanded
    }

    /// <summary>
    /// The weapon classification
    /// </summary>
    public Types Type { get; }

    public MeleeWeapon(string id, string name, string desc, Sprite sprite, int cost, int damage, float range, 
        float time, Types type) 
        : base(id, name, desc, sprite, cost, damage, range, time) {
        
        Type = type;
    }
}
