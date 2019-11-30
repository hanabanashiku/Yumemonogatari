using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yumemonogatari.Items {
    /// <summary>
    /// Represents a NPC store front.
    /// </summary>
    [Serializable]
    public class Store : MonoBehaviour {
        public List<Item> items;
        public Dictionary<Item, int> sold;

        /// <summary>
        /// Buy an item.
        /// </summary>
        /// <param name="inventory">The player's inventory</param>
        /// <param name="item">The item to buy</param>
        /// <param name="quantity">The amount to buy</param>
        /// <exception cref="ArgumentException">If the item isn't for sale or is too expensive.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If an invalid quantity was given.</exception>
        public void Buy(Inventory inventory, Item item, int quantity = 1) {
            if(inventory is null)
                throw new NullReferenceException(nameof(inventory));
            if(item == null)
                throw new NullReferenceException(nameof(item));

            var isBuyback = sold.ContainsKey(item);
            
             if(!items.Contains(item) && !isBuyback)
                throw new ArgumentException(nameof(item));
            if(quantity < 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));
            if(isBuyback && quantity < sold[item])
                throw new ArgumentException(nameof(quantity));

            var price = item.price;
            if(isBuyback)
                price = (int)(price*0.75);
            
            if(inventory.mon < price)
                throw new ArgumentException("The item is too expensive!");

            inventory.mon -= price * quantity;
            inventory.Add(item, quantity);

            if(!isBuyback) return;
            sold[item] -= quantity;
            if(sold[item] < 1)
                sold.Remove(item);
        }

        /// <summary>
        /// Sell an item.
        /// </summary>
        /// <param name="inventory">The player's inventory.</param>
        /// <param name="item">The item to sell.</param>
        /// <param name="quantity">THe amount to sell.</param>
        /// <exception cref="ArgumentException">If the item is not available to sell.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If they tried to sell too many or not enough.</exception>
        public void Sell(Inventory inventory, Item item, int quantity = 1) {
            if(inventory is null)
                throw new NullReferenceException(nameof(inventory));
            if(item == null)
                throw new NullReferenceException(nameof(item));
            if(!inventory.Contains(item))
                throw new ArgumentException(nameof(item));
            if(quantity < 0 || inventory[item] < quantity)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            inventory.Remove(item, quantity);
            sold[item] += quantity;
            inventory.mon += (int)(item.price * quantity * 0.75);
        }

        public IEnumerable<Item> this[Type type] {
            get { return items.Where(x => x.GetType() == type).Union(sold.Keys.Where(x => x.GetType() == type)); }
        }
    }
}