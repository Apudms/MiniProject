using SimpleInventory.Models;

namespace SimpleInventory.Contracts
{
    public interface ITransaction : ICrud<Transaction>
    {
        int GetTotalTransactionCount();
    }
}
