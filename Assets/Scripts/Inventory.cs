using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The player's inventory and equipped items.
/// </summary>
public class Inventory : MonoBehaviour, ICollection<Item>, IEnumerable<Tuple<Item, int>> {
    /// <summary>
    /// The player's items, and the quantity.
    /// </summary>
    public Dictionary<Item, int> items;

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

    /// <summary>
    /// The amount of ammo remaining in a round.
    /// </summary>
    public int remainingAmmo;

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

    /// <summary>
    /// True if the player is currently reloading. Don't allow use of a weapon.
    /// </summary>
    public bool Reloading => _reloadRoutine != null;

    private Coroutine _reloadRoutine;

    /// <summary>
    /// Equip a melee weapon. Pass null to remove.
    /// </summary>
    /// <param name="weapon">The weapon to equip.</param>
    public void EquipWeapon(MeleeWeapon weapon) {
        if(weapon == null) {
            _melee = null;
            return;
        }

        if(!Contains(weapon)) {
            Debug.Log($"The user's inventory does not contain {weapon.name}");
            return;
        }

        _melee = weapon;
    }

    /// <summary>
    /// Equip a ranged weapon. Pass null to remove.
    /// </summary>
    /// <param name="weapon">The weapon to equip.</param>
    public void EquipWeapon(RangedWeapon weapon) {
        if(weapon == null) {
            _ranged = null;
            ResetAmmo();
            return;
        }

        if(!Contains(weapon)) {
            Debug.Log($"The user's inventory does not contain {weapon.name}");
            return;
        }

        _ranged = weapon;
    }

    /// <summary>
    /// Reload the current ranged weapon.
    /// </summary>
    public void Reload() {
        if(_ranged == null)
            return;
        _reloadRoutine = StartCoroutine(ReloadAsync());
    }

    /// <summary>
    /// Interrupt the reloading process early without changing the values.
    /// </summary>
    public void InterruptReload() {
        if(!Reloading)
            return;
        StopCoroutine(_reloadRoutine);
        _reloadRoutine = null;
    }

    private IEnumerator ReloadAsync() {
        var ammo = (_ranged.type == RangedWeapon.Types.Gun) ? bullets : arrows;

        if(ammo == 0) {
            _reloadRoutine = null;
            yield break;
        }
        
        yield return new WaitForSeconds(_ranged.reloadSpeed);
        // update just in case.
        ammo = (_ranged.type == RangedWeapon.Types.Gun) ? bullets : arrows;

        // The amount of ammo we want to load
        var reloadAmount = _ranged.ammoPerRound - remainingAmmo;

        if(reloadAmount < ammo)
            reloadAmount = ammo;

        ammo -= reloadAmount;
        remainingAmmo += reloadAmount;

        if(_ranged.type == RangedWeapon.Types.Gun)
            bullets = ammo;
        else
            arrows = ammo;
        _reloadRoutine = null;
    }

    // reset current ammo if the weapon is dequipped.
    private void ResetAmmo() {
        if(_ranged == null)
            return;
        InterruptReload();
        
        if(_ranged.type == RangedWeapon.Types.Bow)
            arrows += remainingAmmo;
        else
            bullets += remainingAmmo;

        remainingAmmo = 0;
    }

    /// <summary>
    /// Equip an armor. Pass null to remove.
    /// </summary>
    /// <param name="armor">The armor to equip.</param>
    public void EquipArmor(Armor armor) {
        if(armor == null) {
            _equippedArmor = null;
            return;
        }

        if(!Contains(armor)) {
            Debug.Log($"The user's inventory does not contain {armor.name}");
            return;
        }

        _equippedArmor = armor;
    }

    public Inventory() {
        items = new Dictionary<Item, int>();
    }

    IEnumerator<Tuple<Item, int>> IEnumerable<Tuple<Item, int>>.GetEnumerator() {
        return items.Keys.Select(x => new Tuple<Item, int>(x, items[x])).GetEnumerator();
    }

    public IEnumerator<Item> GetEnumerator() {
        return items.Keys.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public void Add(Item item) {
        if (item == null) {
            Debug.LogWarning("Adding null item to inventory.");
            return;
        }
            
        items[item] += 1;
    }

    public void Add(Item item, int quantity) {
        if(quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));
        
        items[item] += quantity;
    }

    public void Clear() {
        throw new NotSupportedException();
    }

    public bool Contains(Item item) {
        if (item == null)
            return false;
        return items.ContainsKey(item);
    }

    public void CopyTo(Item[] array, int arrayIndex) {
        items.Keys.CopyTo(array, arrayIndex);
    }

    public bool Remove(Item item) {
        if (item == null)
            return false;
        return items.Remove(item);
    }

    public bool Remove(Item item, int quantity) {
        if(quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        if (!items.ContainsKey(item))
            return false;

        var q = items[item];
        if (q >= quantity)
            return Remove(item);

        items[item] -= quantity;
        return true;
    }

    /// <summary>
    /// Get and set the quantity of an item
    /// </summary>
    /// <param name="i">The item</param>
    /// <exception cref="ArgumentOutOfRangeException">If the quantity is out of the range [0, 999]</exception>
    public int this[Item i] {
        get {
            if(!Contains(i))
                return 0;
            return items[i];
        }
        set {
            if(value < 0 || value > 999)
                throw new ArgumentOutOfRangeException(nameof(value));
            items[i] = value;
        }
    }

    public IEnumerable<Tuple<Item, int>> this[Type t] {
        get { return items.Keys.Where(x => x.GetType() == t).Select(x => new Tuple<Item, int>(x, items[x])); }
    }

    public int Count => items.Count;
    public bool IsReadOnly => false;
}
