using System;
using System.Collections.Generic;
using System.Text;

using GlossyCommerce.Shared.Models;

namespace GlossyCommerce.Shared.Services
{
    public class CartService
    {
        // Holds the items currently in the cart
        public List<OrderItem> CartItems { get; private set; } = new();

        // Event to notify the UI when the cart changes (so the cart counter updates instantly)
        public event Action? OnCartChanged;

        public void AddToCart(Product product)
        {
            var existingItem = CartItems.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = 1,
                    UnitPrice = product.Price
                });
            }

            OnCartChanged?.Invoke();
        }

        public decimal GetTotal() => CartItems.Sum(i => i.UnitPrice * i.Quantity);

        public void ClearCart()
        {
            CartItems.Clear();
            OnCartChanged?.Invoke();
        }
        public void RemoveFromCart(int productId)
        {
            var item = CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                CartItems.Remove(item);
                OnCartChanged?.Invoke(); // Instantly updates the UI
            }
        }
    }
}