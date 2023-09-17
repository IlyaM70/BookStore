using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.RepositoryInterface;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _categoryRepo.GetAll().ToList();
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
                _categoryRepo.Add(category);
                _categoryRepo.Save();
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
            Category? categoryFromDb = _categoryRepo.GetFirstOrDefault(c=>c.Id==id);
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
                _categoryRepo.Update(category);
                _categoryRepo.Save();
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
            Category? categoryFromDb = _categoryRepo.GetFirstOrDefault(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? categoryFromDb = _categoryRepo.GetFirstOrDefault(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(categoryFromDb);
            _categoryRepo.Save();
            TempData["success"] = $"Category {categoryFromDb.Name} deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

