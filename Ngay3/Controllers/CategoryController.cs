using Microsoft.AspNetCore.Mvc;
using Ngay3.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ngay3.Controllers
{
    public class CategoryController : Controller
    {
        // Danh sách Category dùng chung
        public static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại" },
            new Category { Id = 2, Name = "Laptop" },
            new Category { Id = 3, Name = "Phụ kiện" }
        };

        public ActionResult Index()
        {
            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = categories.Any()
                    ? categories.Max(c => c.Id) + 1
                    : 1;

                categories.Add(category);

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public ActionResult Delete(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            // Kiểm tra Category có Product hay chưa
            bool exists = ProductController.products
                .Any(p => p.CategoryId == id);

            if (exists)
            {
                return BadRequest("Không thể xóa Category vì đang có sản phẩm sử dụng!");
            }

            categories.Remove(category);

            return RedirectToAction(nameof(Index));
        }
    }
}