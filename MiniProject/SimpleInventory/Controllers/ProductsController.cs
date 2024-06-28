using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleInventory.Contracts;
using SimpleInventory.Models;
using SimpleInventory.Services;

namespace SimpleInventory.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _product;
        private readonly InventoryDbContext _context;

        public ProductsController(InventoryDbContext context, IProduct product)
        {
            _product = product;
            _context = context;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            IEnumerable<Product> products;
            products = _product.GetAll();
            return View(products);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            var product = _product.GetById(id);
            return View(product);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                var result = _product.Add(product);
                TempData["Message"] = $"Product {product.ProductId} added successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ErrorMessage = "Failed to add new product";
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                var result = _product.Update(product);

                TempData["Message"] = $"Product with ID: {product.ProductId}, updated successfully";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.ErrorMessage = $"Failed to edit product with ID: {product.ProductId}";
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ProductsController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
