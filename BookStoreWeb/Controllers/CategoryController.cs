using BookStore.DataAccess.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = $"Category {category.Name} created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id== null||id==0) 
            {
                return NotFound();
            }
            Category? categoryFromDb =  _db.Categories.Where(c=>c.Id==id).FirstOrDefault();
            if (categoryFromDb==null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = $"Category {category.Name} updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? categoryFromDb = _db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            TempData["success"] = $"Category {categoryFromDb.Name} deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

