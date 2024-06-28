using SimpleInventory.Contracts;
using SimpleInventory.Models;

namespace SimpleInventory.Services
{
    public class TransactionService : ITransaction
    {
        private readonly InventoryDbContext _context;

        public TransactionService(InventoryDbContext context)
        {
            _context = context;
        }

        public Transaction Add(Transaction entity)
        {
            try
            {
                _context.Transactions.Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var delTran = GetById(id);
                if (delTran != null)
                {
                    _context.Transactions.Remove(delTran);
                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"Failed to delete trans {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Transaction> GetAll()
        {
            var results = _context.Transactions.ToList();
            return results;
        }

        public Transaction GetById(int id)
        {
            var result = _context.Transactions.Where(t => t.TransactionId == id).FirstOrDefault();
            if (result == null)
            {
                throw new ArgumentException("Transaction not found");
            }
            return result;
        }

        public Transaction Update(Transaction entity)
        {
            try
            {
                var updTran = GetById(entity.TransactionId);
                if (updTran != null)
                {
                    updTran.TransactionId = entity.TransactionId;
                    updTran.ProductId = entity.ProductId;
                    updTran.TransactionType = entity.TransactionType;
                    updTran.Quantity = entity.Quantity;
                    updTran.Date = entity.Date;
                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"Failed to update transaction with ID: {entity.TransactionId}");
                }
                return updTran;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
