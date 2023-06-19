using Microsoft.AspNetCore.Mvc;
using UsersApp.Data;
using UsersApp.Models;

namespace UsersApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db. Categories.ToList();
            return View(objCategoryList);
        }
        
        //Get
        public IActionResult Create()
        {
          
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj) 
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The displayorder does not match name");
            }
                if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //Get
        public IActionResult Edit(int? id)
        {
            if(id== null || id==0) {
                return NotFound(); 
            }
            var categoryFromDb = _db.Categories.Find(id);
           // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
           // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound(); 
            }

            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The displayorder does not match name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            // var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }





        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
      
                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category deleted succesfully";
            return RedirectToAction("Index");
            
        }


    }
}
