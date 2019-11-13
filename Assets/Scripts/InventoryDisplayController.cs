using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the display for the pause menu inventory editor.
/// </summary>
[RequireComponent(typeof(GridLayoutGroup))]
public class InventoryDisplayController : MonoBehaviour {

    public PlayerCharacter player; //The player
    public Inventory inventory; // The player's inventory
    public Transform grid; // The icon grid
    public Transform navPointer; // The item type marker
    public Text itemName;
    public Text itemDescription;
    public GameObject meleeStats;
    public Text meleeDamageStat;
    public GameObject rangedStats;
    public Text rangedDamageStat;
    public Text rangedAmmoStat;
    public Text rangedRangeStat;
    public GameObject armorStats;
    public Text armorShieldStat;
    public Text armorRechargeStat;
    public GameObject consumableStats;
    public Text consumableHealthStat;
    public Text consumableShieldStat;
    
    private List<ItemDisplayController> _gridItems; // The current list of object icons
    private Dictionary<Pages, List<ItemDisplayController>> _lists; // the cached object icons
    private ItemDisplayController _selected; // The currently selected icon
    private Pages _currentPage; // The current page
    private int _maxColumns; // The maximum number of columns in the UI
    
    public enum Pages { Melee = 0, Ranged = 1, Armor = 2, Consumable = 3 }

    private void Awake() {
        _gridItems = new List<ItemDisplayController>();
        player = FindObjectOfType<PlayerCharacter>();
        inventory = player.inventory;
        
        // cache the icons and data we need
        _lists = new Dictionary<Pages, List<ItemDisplayController>> {
            [Pages.Melee] = inventory[typeof(MeleeWeapon)].Select(x => LoadItem(x.Item1, x.Item2)).ToList(),
            [Pages.Ranged] = inventory[typeof(RangedWeapon)].Select(x => LoadItem(x.Item1, x.Item2)).ToList(),
            [Pages.Armor] = inventory[typeof(Armor)].Select(x => LoadItem(x.Item1, x.Item2)).ToList(),
            [Pages.Consumable] = inventory[typeof(Consumable)].Select(x => LoadItem(x.Item1, x.Item2)).ToList()
        };

        ChangePage(_currentPage);
        if(_selected == null && _gridItems.Count > 0)
            SelectItem(_gridItems[0]);

        _maxColumns = grid.gameObject.GetComponentInChildren<GridLayoutGroup>().constraintCount;
    }

    private void Update() {
        if(Input.GetButtonUp("Submit")) {
            ActivateItem(_selected);
            return;
        }

        var (mC, _) = GetRowColumnCount();
        var (c, r) = GetCurrentRowColumn();
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var hAxis = Mathf.Abs(h) >= Mathf.Abs(v);

        // right arrow key
        if(hAxis && h > 0) {
            c++;
            // changing the page
            if(c > mC) {
                if(_currentPage != Pages.Consumable)
                    ChangePage(_currentPage + 1);
                
                SelectItem(c, r);
            }
        }
        
        // left arrow key
        else if(hAxis && h < 0) {
            c--;
            // changing the page
            if(c < 1) {
                if(_currentPage != Pages.Melee)
                    ChangePage(_currentPage - 1);
            }
            
            SelectItem(GetItem(c, r));
        }

        // Up arrow key
        else if(v > 0) {
            r++;
            SelectItem(c, r);  
        }

        else if(v < 0) {
            r--;
            SelectItem(c, r);
        }

    }

    // display the item as selected and save the reference
    private void SelectItem(ItemDisplayController idc) {
        _selected.Deselect();

        if(idc is null) {
            ResetItemData();
            itemName.text = string.Empty;
            itemDescription.gameObject.SetActive(false);
            return;
        }
        
        idc.Select();
        _selected = idc;

        var item = (Item)idc;
        itemName.text = item.name;
        itemDescription.gameObject.SetActive(true);
        itemDescription.text = item.description;
        
        if(item.GetType() == typeof(MeleeWeapon))
            DisplayItemData((MeleeWeapon)item);
        else if(item.GetType() == typeof(RangedWeapon))
            DisplayItemData((RangedWeapon)item);
        else if(item.GetType() == typeof(Armor))
            DisplayItemData((Armor)item);
        else if(item.GetType() == typeof(Consumable))
            DisplayItemData((Consumable)item);
    }

    /// <summary>
    /// Select the item in the given column and row.
    /// </summary>
    /// <param name="c">The one-based column</param>
    /// <param name="r">The one-based row</param>
    /// <remarks>This function resolves inconsistencies (e.g. row of -1 or column of 6)</remarks>
    private void SelectItem(int c, int r) {
        var (mC, mR) = GetRowColumnCount();
        var n = _gridItems.Count;
        // nothing to select!
        if(n == 0) {
            _selected = null;
            return;
        }

        // went over columns - loop to beginning
        if(c > mC)
            c = 1;
        
        // went under columns - loop to end
        else if(c < 0) {
            // if we're on the last row, it may not be full.
            if(r == mR)
                c = n - (mR - 1) * mC; // the last column in the last row.
            else
                c = mC;
        }
        if(r > mR)
            r = 1;
        else if(r < 0)
            r = mR;

        SelectItem(GetItem(c, r));
    }

    private void ResetItemData() {
        meleeStats.SetActive(false);
        rangedStats.SetActive(false);
        armorStats.SetActive(false);
        consumableStats.SetActive(false);
    }
    
    private void DisplayItemData(MeleeWeapon item) {
        ResetItemData();    
        meleeStats.SetActive(true);
        meleeDamageStat.text = $"{item.damage}";
    }

    private void DisplayItemData(RangedWeapon item) {
        ResetItemData();
        rangedStats.SetActive(true);
        rangedDamageStat.text = $"{item.damage}";
        rangedAmmoStat.text = $"{item.ammoPerRound}";
        rangedRangeStat.text = $"{item.range} m";
    }

    private void DisplayItemData(Armor item) {
        ResetItemData();
        armorStats.SetActive(true);
        armorShieldStat.text = $"{item.shield}";
        armorRechargeStat.text = $"{item.rechargeSpeed} hp/s";
    }

    private void DisplayItemData(Consumable item) {
        ResetItemData();
        consumableStats.SetActive(true);
        consumableHealthStat.text = $"{item.health}";
        consumableShieldStat.text = $"{item.shield}";
    }
   
    /// <summary>
    /// Change the page and populate it.
    /// </summary>
    /// <param name="page">The new page.</param>
    private void ChangePage(Pages page) {
        if(_currentPage != page) {
            var p = navPointer.localPosition;
            p.x = 7 + 70 * (int)page; // move the marker under the right page.
            navPointer.localPosition = p;
        }
        
        Clear();
        _currentPage = page;
        Populate();
    }

    /// <summary>
    /// Populate the current page with items.
    /// </summary>
    /// <remarks>Should only be called once.</remarks>
    private void Populate() {
        _gridItems = _lists[_currentPage];
        foreach(var i in _gridItems) {
            i.transform.SetParent(grid);
            i.gameObject.SetActive(true);
        }
    }

    // instantiate and load an item icon
    private ItemDisplayController LoadItem(Item item, int quantity) {
        var obj = new GameObject();
        var idc = obj.AddComponent<ItemDisplayController>();
        idc.item = item;
        idc.SetQuantity(quantity);
        obj.SetActive(false);
        obj.transform.SetParent(grid);
        return idc;
    }

    // Remove item from grid
    private void RemoveItem(ItemDisplayController item) {
        _gridItems.Remove(item);
        item.Delete();
    }

    /// <summary>
    /// Delete a given number of items from the inventory.
    /// </summary>
    /// <param name="item">The item</param>
    /// <param name="quantity">The number to delete</param>
    private void DeleteItem(ItemDisplayController item, int quantity) {
        var q = inventory[item];

        if(q <= quantity)
            RemoveItem(item);
        inventory.Remove(item, quantity);
    }

    /// <summary>
    /// Called if the user hit the activate
    /// </summary>
    /// <param name="idc">The item to equip, dequip, or consume</param>
    private void ActivateItem(ItemDisplayController idc) {
        var item = (Item)idc;
        
        // find the type
        // unequip if it is equipped,
        // otherwise, dequip the current item in the UI if applicable
        // and equip the new item in the inventory and the UI.
        if(item.GetType() == typeof(MeleeWeapon)) {
            if(inventory.EquippedMeleeWeapon == item)
                inventory.EquipWeapon((MeleeWeapon)null);
            else {
                _gridItems.Find(x => x.item == inventory.EquippedMeleeWeapon)?.Dequip();
                inventory.EquipWeapon((MeleeWeapon)item);
                idc.Equip();
            }
        }
        
        else if(item.GetType() == typeof(RangedWeapon)) {
            if(inventory.EquippedRangedWeapon == item)
                inventory.EquipWeapon((RangedWeapon)null);
            else {
                _gridItems.Find(x => x.item == inventory.EquippedRangedWeapon)?.Dequip();
                inventory.EquipWeapon((RangedWeapon)item);
                idc.Equip();
            }
        }
        
        else if(item.GetType() == typeof(Armor)) {
            if(inventory.EquippedArmor == item)
                inventory.EquipArmor(null);
            else {
                _gridItems.Find(x => x.item == inventory.EquippedArmor)?.Dequip();
                inventory.EquipArmor((Armor)item);
                idc.Equip();
            }
            
        }
        
        // probably a consumable
        else
            Consume(idc);
    }

    private void Consume(ItemDisplayController idc) {
        if(idc.item.GetType() != typeof(Consumable))
            throw new ArgumentException(nameof(idc));

        var item = (Consumable)idc;
        player.Consume(item);
        DeleteItem(idc, 1);
    }
    
    private void Clear() {
        foreach(var i in _gridItems) {
            i.gameObject.SetActive(false);
            //i.transform.SetParent(null);
        }
    }

    private Tuple<int, int> GetRowColumnCount() {
        var n = _gridItems.Count;
        var c = Math.Min(_maxColumns, n);
        var r = (int)Math.Ceiling((double)n / c);
        return new Tuple<int, int>(c, r);
    }

    private Tuple<int, int> GetCurrentRowColumn() {
        var i = _gridItems.IndexOf(_selected) + 1;
        var r = (int)Math.Ceiling((double)i / _maxColumns);
        var c = i - ((r - 1) * _maxColumns);
        return new Tuple<int, int>(c, r);
    }

    private ItemDisplayController GetItem(int c, int r) {
        var i = (r - 1) * _maxColumns + c;
        i--;
        return _gridItems[i];
    }
}