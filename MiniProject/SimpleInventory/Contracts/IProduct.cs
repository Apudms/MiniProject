using SimpleInventory.Models;

namespace SimpleInventory.Contracts
{
    public interface IProduct : ICrud<Product>
    {
        int GetTotalProductCount();
    }
}
