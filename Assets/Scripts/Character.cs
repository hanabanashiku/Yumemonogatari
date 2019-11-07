using System;
using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    protected Animator animator;
    protected Rigidbody2D body;
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
    public int MaxShield => (inventory.EquippedArmor != null) ? inventory.EquippedArmor.shield : 0;

    /// <summary>
    /// The current amount of shield left.
    /// </summary>
    public int shield;

    protected const float WalkSpeed = 5.0f;
    protected const float RunSpeed = 9.0f;

    protected virtual void Awake() {
        animator = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Damage the character.
    /// </summary>
    /// <param name="damage">The amount of damage to deal.</param>
    public virtual void TakeDamage(int damage) {
        // interrupt shield
        InterruptShield();

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

    // Look for attacks
    public void OnTriggerEnter2D(Collider2D c) {
        var item = c.gameObject.GetComponent<Item>();
        var proj = c.gameObject.GetComponent<Projectile>();
        
        // hit with a melee weapon.
        if (item.GetType() == typeof(MeleeWeapon)) {
            var w = (MeleeWeapon)item;
            TakeDamage(w.damage);
        }
        
        // hit with a projectile
        else if (proj != null) {
            var w = (RangedWeapon) item;
            // take a critical if the head is hit.
            if(c is CircleCollider2D)
                TakeDamage((int)Math.Ceiling(proj.weapon.damage * Weapon.CriticalMultiplier));
            else TakeDamage(proj.weapon.damage);
        }
    }

    protected IEnumerator RechargeShield() {
        var armor = inventory.EquippedArmor;
        if(armor is null || armor.shield == shield)
            yield break;
        
        yield return new WaitForSeconds(armor.rechargeDelay);

        // recharge the shield six times a second until full.
        var inc = armor.rechargeSpeed / 6;
        while(shield < armor.shield) {
            shield = Math.Min(shield + inc, armor.shield);
            yield return new WaitForSeconds(1/6f);
        }

        // conclude coroutine and destroy pointer
        _shieldRecharge = null;
    }

    protected void InterruptShield() {
        if (_shieldRecharge == null) 
            return;
        StopCoroutine(_shieldRecharge);
        _shieldRecharge = null;
    }

    public abstract void OnDeath();

}
