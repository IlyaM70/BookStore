using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.RepositoryInterface;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {

            Category category = new Category();

            if (id != null && id != 0)
            {
                //update page
                category = _unitOfWork.Category.Get(p => p.Id == id);
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                string action = "";
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                    action = "created";
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                    action = "edited";
                }
                _unitOfWork.Save();
                TempData["success"] = $"Category {category.Name} {action} successfully";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll().OrderBy(c => c.DisplayOrder).ToList();
            return Json(new { data = categoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Category? categoryFromDb = _unitOfWork.Category.Get(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return Json(new { success = false, message = $"Error when deleting" });
            }
            _unitOfWork.Category.Remove(categoryFromDb);
            _unitOfWork.Save();
            
            return Json(new { success = true, message = $"Category {categoryFromDb.Name} deleted successfully" });
        }
        #endregion
    }
}

