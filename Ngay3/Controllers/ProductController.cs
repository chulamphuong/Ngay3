
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ngay3.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ngay3.Controllers
{
    public class ProductController : Controller
    {
        // Danh sách Product
        public static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone 15", Price = 20000000, Description = "Điện thoại Apple", CategoryId = 1 },
            new Product { Id = 2, Name = "Samsung S24", Price = 18000000, Description = "Điện thoại Samsung", CategoryId = 1 },
            new Product { Id = 3, Name = "Dell Inspiron", Price = 15000000, Description = "Laptop Dell", CategoryId = 2 }
        };

        // Kiểm tra Category có tồn tại không
        private bool CheckCategory(int categoryId)
        {
            return CategoryController.categories
                .Any(c => c.Id == categoryId);
        }

        // Đưa danh sách Category vào Dropdown
        private void LoadCategories(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(
                CategoryController.categories,
                "Id",
                "Name",
                selectedId
            );
        }

        // GET: Product/Index
        public ActionResult Index(int? id)
        {
            ViewBag.Categories = CategoryController.categories;

            ViewBag.ProductIds = new SelectList(
                products.Select(p => p.Id),
                id
            );

            var result = id.HasValue
                ? products.Where(p => p.Id == id.Value).ToList()
                : products;

            return View(result);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            LoadCategories();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            // Kiểm tra Category
            if (!CheckCategory(product.CategoryId))
            {
                ModelState.AddModelError(
                    "CategoryId",
                    "Mã Category không tồn tại! Vui lòng chọn Category có sẵn."
                );
            }

            if (ModelState.IsValid)
            {
                // Tạo ID tự động
                product.Id = products.Any()
                    ? products.Max(p => p.Id) + 1
                    : 1;

                products.Add(product);

                return RedirectToAction(nameof(Index));
            }

            LoadCategories(product.CategoryId);
            return View(product);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            ViewBag.CategoryName = CategoryController.categories
                .FirstOrDefault(c => c.Id == product.CategoryId)?.Name;

            if (ViewBag.CategoryName == null)
                return NotFound("Không tìm thấy Category của sản phẩm!");

            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            LoadCategories(product.CategoryId);

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            var oldProduct = products.FirstOrDefault(p => p.Id == id);

            if (oldProduct == null)
                return NotFound();

            // Kiểm tra Category tồn tại
            if (!CheckCategory(product.CategoryId))
            {
                ModelState.AddModelError(
                    "CategoryId",
                    "Mã Category không tồn tại!"
                );
            }

            if (ModelState.IsValid)
            {
                oldProduct.Name = product.Name;
                oldProduct.Price = product.Price;
                oldProduct.Description = product.Description;
                oldProduct.CategoryId = product.CategoryId;

                return RedirectToAction(nameof(Index));
            }

            LoadCategories(product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            products.Remove(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
