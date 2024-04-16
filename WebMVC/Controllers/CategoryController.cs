using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Data;
using WebMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            List<Category> results = _appDbContext.Categories.ToList();

            return View(results);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Store(Category category)
        {
            if (_appDbContext.Categories.Any(d => d.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Category name must be unique");
            }

            if (!ModelState.IsValid)
            {
                return View("Create", category);
            }

            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index", "Category");
        }
    }
}

