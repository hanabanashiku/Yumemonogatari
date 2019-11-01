using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Items {
    private static List<Item> _items;

    public static IQueryable<Ammunition> Ammunition => _items.Where(x => x.GetType() == typeof(Ammunition)).Select(x => (Ammunition)x).AsQueryable();    
    public static IQueryable<Armor> Armor = _items.Where(x => x.GetType() == typeof(Armor)).Select(x => (Armor)x).AsQueryable();
    public static IQueryable<Consumable> Consumables => _items.Where(x => x.GetType() == typeof(Consumable)).Select(x => (Consumable)x).AsQueryable();
    public static IQueryable<MeleeWeapon> MeleeWeapons => _items.Where(x => x.GetType() == typeof(MeleeWeapon)).Select(x => (MeleeWeapon)x).AsQueryable();
    public static IQueryable<RangedWeapon> RangedWeapons => _items.Where(x => x.GetType() == typeof(RangedWeapon)).Select(x => (RangedWeapon)x).AsQueryable();

    public static IQueryable<Item> Queryable => _items.AsQueryable();

    public static TItem GetById<TItem>(string identifier) where TItem : Item {
        return (TItem)_items.FirstOrDefault(x => x.identifier == identifier);
    }

    public static Item GetById(string identifier) {
        return _items.FirstOrDefault(x => x.identifier == identifier);
    }

    private static List<Item> ItemList {
        get {
            if(_items == null) {
                _items = new List<Item>(Resources.FindObjectsOfTypeAll<Item>());
            }

            return _items;
        }
    }
    


}