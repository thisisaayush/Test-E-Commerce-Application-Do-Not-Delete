using E_commerce_Application.Data;
using E_commerce_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_Application.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index() //goes to index.cshtml class in Views/Category folder.
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Category Name and Display Order is same!");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
            
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int ? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category categoryFromDb = _db.Categories.Find(id);
            
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);

        }
    }
}
