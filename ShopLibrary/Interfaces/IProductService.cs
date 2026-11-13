using ShopLibrary.Models;
namespace ShopLibrary.Interfaces
{
    public interface IProductService
    {
        void AddStock(Product product, int quantity);
        void Sell(Product product, int quantity);
        decimal GetTotalCost(Product product);
    }
}