using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The player's inventory and equipped items.
/// </summary>
public class Inventory {
    /// <summary>
    /// The player's items
    /// </summary>
    public List<Item> Items;

    /// <summary>
    /// The amount of currency held by the player.
    /// </summary>
    public int mon;

    /// <summary>
    /// The number of arrows the player has.
    /// </summary>
    public int arrows;

    /// <summary>
    /// The number of bullets the player has.
    /// </summary>
    public int bullets;

    private Armor _equippedArmor;
    /// <summary>
    /// The armor that is currently equipped.
    /// </summary>
    public Armor EquippedArmor => _equippedArmor;

    private RangedWeapon _ranged;
    /// <summary>
    /// The bow or gun that is currently equipped.
    /// </summary>
    public RangedWeapon EquippedRangedWeapon => _ranged;

    private MeleeWeapon _melee;
    /// <summary>
    /// The sword that is currently equipped.
    /// </summary>
    public MeleeWeapon EquippedMeleeWeapon => _melee;
    
    public void EquipWeapon(MeleeWeapon weapon){ }
    
    public void EquipWeapon(RangedWeapon weapon) { }
    
    public void EquipArmor(Armor armor) {}
}
