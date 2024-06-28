using SimpleInventory.Contracts;
using SimpleInventory.Models;

namespace SimpleInventory.Services
{
    public class ProductService : IProduct
    {
        private readonly InventoryDbContext _context;

        public ProductService(InventoryDbContext context)
        {
            _context = context;
        }

        public Product Add(Product entity)
        {
            try
            {
                _context.Products.Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var delProd = GetById(id);
                if (delProd != null)
                {
                    _context.Products.Remove(delProd);
                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"Failed to delete product with ID {id}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            var results = _context.Products.ToList();
            return results;
        }

        public Product GetById(int id)
        {
            var result = _context.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (result == null)
            {
                throw new ArgumentException("Product not found");
            }
            return result;
        }

        public int GetTotalProductCount()
        {
            return _context.Products.Count();
        }

        public Product Update(Product entity)
        {
            try
            {
                var updProd = GetById(entity.ProductId);
                if (updProd != null)
                {
                    updProd.ProductId = entity.ProductId;
                    updProd.Name = entity.Name;
                    updProd.StockLevel = entity.StockLevel;
                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"Failed to update product with ID: {entity.ProductId}");
                }
                return updProd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
