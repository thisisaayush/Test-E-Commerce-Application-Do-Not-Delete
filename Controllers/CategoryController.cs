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
    }
}
