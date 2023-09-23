using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.RepositoryInterface;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id) //Update+Insert=Upsert
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
            
            ProductViewModel productVM = new()
            {
                CategoryList = categoryList,
                Product = new Product()
            };

            if (id != null && id != 0)
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(p => p.Id == id);
            }

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productVM,IFormFile? file)
        {
            // check file then creating
            if (file != null ||
                //update
                (file==null && productVM.Product.ImageUrl!=null))
                ModelState.Remove("Product.ImageUrl");

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                string action = "";
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                    action = "created";

                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                    action = "edited";
                }
                _unitOfWork.Save();
                TempData["success"] = $"Product {productVM.Product.Title} {action} successfully";
                return RedirectToAction("Index");
            }

            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
            productVM.CategoryList = categoryList;

            return View(productVM);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? productFromDb = _unitOfWork.Product.Get(c => c.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(productFromDb);
            _unitOfWork.Save();
            TempData["success"] = $"Product {productFromDb.Title} deleted successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data=productList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product productToDelete = _unitOfWork.Product.Get(p=>p.Id==id);
            if(productToDelete== null)
            {
                return Json(new { success=false,message="Error while deleting" });
            }

            var oldImagePath =
                Path.Combine(_webHostEnvironment.ContentRootPath, productToDelete.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productToDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = $"Product {productToDelete.Title} deleted successfully" });
        }
        #endregion
    }

}

