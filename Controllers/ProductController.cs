using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Application.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public ProductController(IUnitOfWork unitofWork)
        {
            _unitofwork = unitofWork;
        }
        public IActionResult Index() //goes to index.cshtml class in Views/Category folder.
        {
            List<Product> objProductList = _unitofwork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (obj.ISBN == obj.Author.ToString())
            {
                ModelState.AddModelError("name", "Category Name and Display Order is same!");
            }

            if (ModelState.IsValid)
            {
                _unitofwork.Product.Add(obj);
                _unitofwork.Save();   
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View(); 
        }

        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category categoryFromDb = _unitofwork.Category.Get(u=>u.Id==id);
            
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Update(obj);
                _unitofwork.Save();
                TempData["success"] = "Category edited successfully!";
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

            Category categoryFromDb = _unitofwork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitofwork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofwork.Category.Remove(obj);
            _unitofwork.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }
             
    }
}
