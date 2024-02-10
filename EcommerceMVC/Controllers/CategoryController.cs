using EcommerceMVC.Data;
using EcommerceMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationDbContext db;
        public CategoryController(ApplicationDbContext _db)
        {
            db = _db; 
        }
        public IActionResult Index()
        {
            List<Category> categoryList = db.Categories.ToList();
            return View(categoryList);
        }
        public IActionResult Create()
        {           
            return View();
        }
    }
}
