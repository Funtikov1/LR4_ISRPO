using System;
using ShopLibrary.Models;
using ShopLibrary.Exceptions;
using ShopLibrary.Interfaces;
namespace ShopLibrary.Services
{
    public class ProductService : IProductService
    {
        public void AddStock(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Товар не указан.");
            if (quantity <= 0)
                throw new InvalidProductException("Количество должно быть больше нуля.");
            if (product.Quantity < 0)
                throw new InvalidProductException("Остаток товара не может быть отрицательным.");
            product.Quantity += quantity;
        }
        public void Sell(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Товар не указан.");
            if (quantity <= 0)
                throw new InvalidProductException("Количество должно быть больше нуля.");
            if (product.Quantity < quantity)
                throw new InvalidOperationException("Недостаточно товара для продажи.");
            product.Quantity -= quantity;
        }
        public decimal GetTotalCost(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Товар не указан.");
            if (product.Price < 0)
                throw new InvalidProductException("Цена не может быть отрицательной.");
            if (product.Quantity < 0)
                throw new InvalidProductException("Количество не может быть отрицательным.");
            return product.Price * product.Quantity;
        }
    }
}