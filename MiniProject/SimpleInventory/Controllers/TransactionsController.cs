using Microsoft.AspNetCore.Mvc;
using SimpleInventory.Contracts;
using SimpleInventory.Models;
using SimpleInventory.Services;

namespace SimpleInventory.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransaction _transaction;
        private readonly IProduct _product;

        public TransactionsController(ITransaction transaction, IProduct product)
        {
            _transaction = transaction;
            _product = product;
        }

        // GET: TransactionsController
        public IActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            IEnumerable<Transaction> transactions;
            transactions = _transaction.GetAll();
            return View(transactions);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            var products = _product.GetAll();
            return View(products);
        }

        // POST: TransactionsController/Create
        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _transaction.Add(transaction);
                    TempData["Message"] = $"Transaction with ID: {transaction.TransactionId} added successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Jika model tidak valid, kembali ke tampilan Create dengan model yang sama
                    // Ini akan menampilkan pesan validasi di tampilan
                    IEnumerable<Product> products = _product.GetAll();
                    ViewBag.Products = products;
                    return View(transaction);
                }
            }
            catch (Exception ex)
            {
                // Mengelola kesalahan saat menambahkan transaksi baru
                ViewBag.ErrorMessage = $"Failed to add new transaction: {ex.Message}";
                return View(transaction);
            }
        }
    }
}
