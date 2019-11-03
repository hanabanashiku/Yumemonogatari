using UnityEngine;

/// <summary>
/// Represents an inventory item
/// </summary>
public class Item : ScriptableObject {

    /// <summary>
    /// The internal identifier of the item.
    /// </summary>
    public string identifier;

    /// <summary>
    /// The name of the item
    /// </summary>
    public new string name;

    /// <summary>
    /// A description of the item.
    /// </summary>
    [TextArea(10,15)]
    public string description;

    /// <summary>
    /// The item's image.
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// The cost of the item, in mon.
    /// </summary>
    public int price;

}
