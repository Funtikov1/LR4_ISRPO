using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopLibrary.Models;
using ShopLibrary.Interfaces;
using ShopLibrary.Services;
using System;
using System.Diagnostics;
using ShopLibrary.Exceptions;
namespace ShopLibrary.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddStock_IncreasesQuantity()
        {
            Product product = new Product();
            product.Name = "Тетрадь";
            product.Price = 50;
            product.Quantity = 10;
            IProductService service = new ProductService();
            service.AddStock(product, 5);
            Assert.AreEqual(15, product.Quantity);
        }
        [TestMethod]
        public void Sell_DecreasesQuantity()
        {
            Product product = new Product();
            product.Name = "Тетрадь";
            product.Price = 50;
            product.Quantity = 10;
            IProductService service = new ProductService();
            service.Sell(product, 3);
            Assert.AreEqual(7, product.Quantity);
        }
        [TestMethod]
        public void GetTotalCost_ReturnsCorrectValue()
        {
            Product product = new Product();
            product.Name = "Тетрадь";
            product.Price = 50;
            product.Quantity = 10;
            IProductService service = new ProductService();
            decimal result = service.GetTotalCost(product);
            Assert.AreEqual(500m, result);
        }
        [TestMethod]
        public void AddStock_InvalidQuantity_IsHandled()
        {
            Product product = new Product();
            product.Name = "Тетрадь";
            product.Price = 50;
            product.Quantity = 10;
            IProductService service = new ProductService();
            bool exceptionCaught = false;
            bool finallyExecuted = false;
            try
            {
                service.AddStock(product, -5);
            }
            catch (InvalidProductException ex)
            {
                exceptionCaught = true;
                Assert.AreEqual("Количество должно быть больше нуля.", ex.Message);
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                finallyExecuted = true;
                Debug.WriteLine("Обработка операции завершена.");
            }
            Assert.IsTrue(exceptionCaught);
            Assert.IsTrue(finallyExecuted);
            Assert.AreEqual(10, product.Quantity);
            service.AddStock(product, 5);
            Assert.AreEqual(15, product.Quantity);
        }
        [TestMethod]
        public void Sell_NotEnoughStock_IsHandled()
        {
            Product product = new Product();
            product.Name = "Тетрадь";
            product.Price = 50;
            product.Quantity = 10;
            IProductService service = new ProductService();
            bool exceptionCaught = false;
            try
            {
                service.Sell(product, 15);
            }
            catch (InvalidOperationException ex)
            {
                exceptionCaught = true;
                Assert.AreEqual("Недостаточно товара для продажи.", ex.Message);
                Debug.WriteLine(ex.Message);
            }
            Assert.IsTrue(exceptionCaught);
            Assert.AreEqual(10, product.Quantity);
            service.Sell(product, 3);
            Assert.AreEqual(7, product.Quantity);
        }
        [TestMethod]
        public void GetTotalCost_NegativePrice_IsHandled()
        {
            Product product = new Product();
            product.Name = "Тетрадь";
            product.Price = -50;
            product.Quantity = 10;
            IProductService service = new ProductService();
            bool exceptionCaught = false;
            try
            {
                service.GetTotalCost(product);
            }
            catch (InvalidProductException ex)
            {
                exceptionCaught = true;
                Assert.AreEqual("Цена не может быть отрицательной.", ex.Message);
                Debug.WriteLine(ex.Message);
            }
            Assert.IsTrue(exceptionCaught);
            product.Price = 50;
            decimal result = service.GetTotalCost(product);
            Assert.AreEqual(500m, result);
        }
    }
}