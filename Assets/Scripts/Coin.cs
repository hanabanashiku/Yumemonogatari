using System;
using UnityEngine;

public class Coin : MonoBehaviour {
    
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

    // If the user collides with the coin, delete it and increase their currency.
    private void OnTriggerEnter2D(Collider2D c) {
        Debug.Log("Trigger");
        var hud = FindObjectOfType<HudController>();
        var player = c.gameObject.GetComponent<PlayerCharacter>();
        if(player == null)
            return;
        if(hud == null)
            Debug.Log("Could not find HUD!");
        
        hud.ShowCoins();
        player.inventory.mon += Value;
        Destroy(gameObject);
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