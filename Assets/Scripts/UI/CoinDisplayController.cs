using System;
using UnityEngine;
using UnityEngine.UI;
using Yumemonogatari.Entities;
using Yumemonogatari.Items;

namespace Yumemonogatari.UI {
public class CoinDisplayController : MonoBehaviour {
    private Inventory _inventory;
    public Text ryo;
    public Text mon;

    private void Start() {
        var player = FindObjectOfType<PlayerCharacter>();
        if(player == null) {
            Debug.Log("Could not find player.");
            return;
        }

        _inventory = player.inventory;
    }

    private void Update() {
        var (currentRyo, currentMon) = Coin.ToRyo(_inventory.mon);
        ryo.text = currentRyo.ToString();
        mon.text = currentMon.ToString();
    }
}
}
