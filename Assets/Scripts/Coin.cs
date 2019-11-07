using System;
using UnityEngine;

/// <summary>
/// Represents a coin collectible.
/// </summary>
public class Coin : Collectible<Item> {
    
    public enum Types {
        Genna,
        Kanei,
        Houei,
        Tenpou,
        Ichibuban,
        Nibuban,
        Koban
    }

    public Types type;

    public int Value {
        get {
            switch(type) {
                case Types.Genna:
                    return 1;
                case Types.Kanei:
                    return 4;
                case Types.Houei:
                    return 10;
                case Types.Tenpou:
                    return 100;
                case Types.Ichibuban:
                    return 1000;
                case Types.Nibuban:
                    return 2000;
                case Types.Koban:
                    return 4000;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    protected override void Collect(PlayerCharacter player) {
        var hud = FindObjectOfType<HudController>();
        if(hud == null)
            Debug.Log("Could not find HUD!");
        
        hud.ShowCoins();
        player.inventory.mon += Value;
    }

    /// <summary>
    /// Convert mon to Ryo.
    /// </summary>
    /// <param name="val">The amount of mon to convert.</param>
    /// <returns>A triplet in the form of (ryo, mon)</returns>
    public static Tuple<int, int> ToRyo(int val) {
        var ryo = (int)Math.Floor(val / 4000m);
        var mon = val % 4000;
        return new Tuple<int, int>(ryo, mon);
    }
}