using System;
using System.Collections;
using UnityEngine;
using Yumemonogatari.Items;

namespace Yumemonogatari.Entities {
    /// <summary>
    /// Represents a PC or NPC in the game.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : MonoBehaviour {

        private static readonly int DirectionParam = Animator.StringToHash("Direction");
        private static readonly int SpeedParam = Animator.StringToHash("Speed");

        protected Animator Animator;
        protected Rigidbody2D Body;
        private Coroutine _shieldRecharge; // The coroutine that recharges the shield after a delay.
        protected MeleeHitboxController Melee; // The sword sprite that contains the melee hitbox.
        protected Coroutine WeaponDelayCoroutine; // The coroutine that manages wait time after using a weapon.

        // prefabs for projectile instantiation
        protected static GameObject ArrowProjectile;
        protected static GameObject BulletProjectile;

        /// <summary>
        /// The direction that the character is currently facing.
        /// </summary>
        public Vector2 direction = Vector2.up;

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

        /// <summary>
        /// The default SortingOrder for the character's sprite renderer.
        /// </summary>
        public const int SortOrder = 500;

        protected virtual void Awake() {
            Animator = gameObject.GetComponent<Animator>();
            Body = gameObject.GetComponent<Rigidbody2D>();
            Melee = gameObject.GetComponentInChildren<MeleeHitboxController>(true);
            Debug.Assert(Melee != null);

            if(inventory is null)
                inventory = gameObject.AddComponent<Inventory>();

            if(ArrowProjectile == null)
                ArrowProjectile = AssetBundles.Item.LoadAsset<GameObject>("Projectile_Arrow");
            if(BulletProjectile == null)
                BulletProjectile = AssetBundles.Item.LoadAsset<GameObject>("Projectile_Bullet");
        }

        /// <summary>
        /// Move the character indefinitely.
        /// </summary>
        /// <param name="d">The direction to move.</param>
        /// <param name="sprint">Whether or not to sprint.</param>
        /// <remarks>Once called, the character will move forever in direction d. To stop, call the function with Vector2.zero as d.</remarks>
        public void Move(Vector2 d, bool sprint = false) {
            var speed = sprint ? RunSpeed : WalkSpeed;
            // changing direction
            if(d != direction) {

                if(d == Vector2.left) {
                    Animator.SetInteger(DirectionParam, 2);
                    Animator.SetFloat(SpeedParam,
                        speed); // rewrite so that these things don't happen in the same frame.
                    direction = d;
                }
                else if(d == Vector2.right) {
                    Animator.SetInteger(DirectionParam, 0);
                    Animator.SetFloat(SpeedParam, speed);
                    direction = d;
                }
                else if(d == Vector2.up) {
                    Animator.SetInteger(DirectionParam, 1);
                    Animator.SetFloat(SpeedParam, speed);
                    direction = d;
                }

                else if(d == Vector2.down) {
                    Animator.SetInteger(DirectionParam, 3);
                    Animator.SetFloat(SpeedParam, speed);
                    direction = d;
                }
                // don't update Direction
                // we want to retain the way the character's facing, but use the value below to stop the character.
                else if(d == Vector2.zero) {
                    Animator.SetFloat(SpeedParam, 0);
                }
                else {
                    Debug.Log($"Unexpected direction {d}!");
                }
            }

            Body.velocity = d * speed;
        }

        /// <summary>
        /// Navigate towards a given point, turning on intersections.
        /// </summary>
        /// <param name="location">The location to move to.</param>
        /// <param name="sprint">Whether or not to run.</param>
        public void MoveTo(Vector2 location, bool sprint) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Perform a melee attack.
        /// </summary>
        public virtual void MeleeAttack() {
            var weapon = inventory.EquippedMeleeWeapon;

            // don't attack if there is no melee weapon or if it was just used.
            if(weapon == null || WeaponDelayCoroutine != null)
                return;

            // Perform the attack
            Melee.Activate();

            // Start attack delay
            WeaponDelayCoroutine = StartCoroutine(StartWeaponDelay(weapon.time + weapon.swingTime));
        }

        /// <summary>
        /// Perform a ranged attack.
        /// </summary>
        /// <param name="target">The direction being aimed at.</param>
        public virtual void RangedAttack(Vector2 target) {
            var weapon = inventory.EquippedRangedWeapon;

            // Don't attack if there is no ranged weapon, it was just used, or if the character is out of ammo.
            if(weapon == null || WeaponDelayCoroutine != null || inventory.remainingAmmo == 0)
                return;

            // Instantiate the projectile and let it deal with its own movement.
            var pos = transform.position;
            var obj = Instantiate(weapon.type == RangedWeapon.Types.Bow ? ArrowProjectile : BulletProjectile,
                pos, Quaternion.identity);
            var projectile = obj.GetComponent<Projectile>();
            projectile.weapon = weapon;
            projectile.target = target;

            // Decrement remaining ammo
            inventory.remainingAmmo--;

            // Start attack delay
            WeaponDelayCoroutine = StartCoroutine(StartWeaponDelay(weapon.time));

        }

        /// <summary>
        /// Damage the character.
        /// </summary>
        /// <param name="damage">The amount of damage to deal.</param>
        public virtual void TakeDamage(int damage) {
            // Stop charging the shield and reloading weapon if damage has been taken.
            InterruptShield();
            inventory.InterruptReload();

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

        /// <summary>
        /// Heal the character, up to the maximum.
        /// </summary>
        /// <param name="hp">The health to restore.</param>
        public virtual void Heal(int hp) {
            health = Math.Min(health + hp, maxHealth);
        }

        /// <summary>
        /// Restore the character's shields, up to the maximum.
        /// </summary>
        /// <param name="s">The amount of shield to restore.</param>
        public virtual void RestoreShield(int s) {
            shield = Math.Min(shield + s, MaxShield);
        }

        public virtual void Consume(Consumable item) {
            Heal(item.health);
            RestoreShield(item.shield);
        }

        // Look for attacks
        public void OnTriggerEnter2D(Collider2D c) {
            var meleeController = c.gameObject.GetComponent<MeleeHitboxController>();
            var proj = c.gameObject.GetComponent<Projectile>();

            // hit with a melee weapon.
            if(meleeController != null) {
                TakeDamage(meleeController.Weapon.damage);
            }

            // hit with a projectile
            else if(proj != null) {
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
                RestoreShield(inc);
                yield return new WaitForSeconds(1 / 6f);
            }

            // conclude coroutine and destroy pointer
            _shieldRecharge = null;
        }

        protected void InterruptShield() {
            if(_shieldRecharge == null)
                return;
            StopCoroutine(_shieldRecharge);
            _shieldRecharge = null;
        }

        protected IEnumerator StartWeaponDelay(float time) {
            yield return new WaitForSeconds(time);
            WeaponDelayCoroutine = null;
        }

        /// <summary>
        /// An event handler to be called when the character's health reaches 0.
        /// </summary>
        public abstract void OnDeath();

    }
}