using System;
using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    protected Animator Animator;
    protected Rigidbody2D Body;
    private Coroutine _shieldRecharge;

    /// <summary>
    /// The name of the character.
    /// </summary>
    public string characterName;

    /// <summary>
    /// The backend identifier of the character.
    /// </summary>
    public string identifier;

    /// <summary>
    /// The character's equipment.
    /// </summary>
    public Inventory inventory;
    /// <summary>
    /// The character's current health.
    /// </summary>
    public int health;

    /// <summary>
    /// The character's maximum health.
    /// </summary>
    public int maxHealth;

    /// <summary>
    /// The maximum amount of shield provided by armor.
    /// </summary>
    public int MaxShield => (inventory.EquippedArmor != null) ? inventory.EquippedArmor.Shield : 0;

    /// <summary>
    /// The current amount of shield left.
    /// </summary>
    public int shield;

    protected const float WalkSpeed = 5.0f;
    protected const float RunSpeed = 9.0f;

    protected virtual void Awake() {
        Animator = gameObject.GetComponent<Animator>();
        Body = gameObject.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Damage the character.
    /// </summary>
    /// <param name="damage">The amount of damage to deal.</param>
    public virtual void TakeDamage(int damage) {
        // interrupt shield recharging
        if(_shieldRecharge != null) {
            StopCoroutine(_shieldRecharge);
            _shieldRecharge = null;
        }
        
        health = Math.Max(health - damage, 0);
        
        // Mark a death.
        if(health == 0) {
            OnDeath();
            return;
        }

        // begin to recharge shield
        if(inventory.EquippedArmor != null)
            _shieldRecharge = StartCoroutine(RechargeShield());
    }

    protected IEnumerator RechargeShield() {
        var armor = inventory.EquippedArmor;
        if(armor is null || armor.Shield == shield)
            yield break;
        
        yield return new WaitForSeconds(armor.RechargeDelay);

        // recharge the shield six times a second until full.
        var inc = armor.RechargeSpeed / 6;
        while(shield < armor.Shield) {
            shield = Math.Min(shield + inc, armor.Shield);
            yield return new WaitForSeconds(1/6f);
        }

        // conclude coroutine and destroy pointer
        _shieldRecharge = null;
    }

    public abstract void OnDeath();

}
