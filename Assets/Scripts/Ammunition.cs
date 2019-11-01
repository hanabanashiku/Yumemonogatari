using System;
using UnityEngine;

/// <summary>
/// A bundle of ammunition.
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "Items/Ammunition", fileName = "Ammunition.asset")]
public class Ammunition : Item {
    public enum AmmoTypes {Arrow, Bullet}

    /// <summary>
    /// The type of ammunition.
    /// </summary>
    public AmmoTypes type;
    
    /// <summary>
    /// The amount of ammo that comes in the bundle.
    /// </summary>
    public int amount;
}