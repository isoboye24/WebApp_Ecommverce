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
        [HttpPost]
        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
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
            Category? catFromDB = db.Categories.Where(x => x.ID == id).FirstOrDefault();
            if (catFromDB == null)
            {
                return NotFound();
            }
            else
            {
                return View(catFromDB);
            }
        }
        [HttpPost]
        public IActionResult Edit(Category cat)
        {
            if (cat.Name == cat.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display order cannot exactly match the name.");
            }
            if (cat.Name != null && cat.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Update(cat);
                db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
