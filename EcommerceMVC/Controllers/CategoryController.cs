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
                TempData["success"] = "Category added successfully";
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

        public IActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")] // Must show that the next action a delete action despite different name i.e DeleteCategory which is used so
                                         // that it will not interface with the default Delete of the action above
        public IActionResult DeleteCategory(int? id)
        {
            Category? catFromDB = db.Categories.Find(id);
            if (catFromDB == null)
            {
                return NotFound();
            }
            else
            {
                db.Categories.Remove(catFromDB);
                db.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
        }
    }
}
