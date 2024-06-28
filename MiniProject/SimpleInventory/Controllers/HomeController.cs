using Microsoft.AspNetCore.Mvc;
using SimpleInventory.Contracts;
using SimpleInventory.Models;
using SimpleInventory.Services;
using System.Diagnostics;

namespace SimpleInventory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduct _product;
        private readonly ITransaction _transaction;

        public HomeController(ILogger<HomeController> logger, IProduct product, ITransaction transaction)
        {
            _logger = logger;
            _product = product;
            _transaction = transaction;
        }

        public IActionResult Index()
        {
            int productCount = _product.GetTotalProductCount();
            int transactionCount = _transaction.GetTotalTransactionCount();
            var model = new HomeViewModel
            {
                ProductCount = productCount,
                TransactionCount = transactionCount
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
