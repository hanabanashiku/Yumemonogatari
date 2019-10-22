using UnityEngine;

public class Weapon : Item {
    
    /// <summary>
    /// The damage done on a normal hit.
    /// </summary>
    public int Damage { get; }
    
    /// <summary>
    /// The range of the weapon.
    /// </summary>
    public float Range { get; }
    
    /// <summary>
    /// The time between successive hits.
    /// </summary>
    public float Time { get; }

    public Weapon(string id, string name, string desc, Sprite sprite, int cost, int damage, float range, float time) 
        : base(id, name, desc, sprite, cost) {
        Damage = damage;
        Range = range;
        Time = time;
    }
}
