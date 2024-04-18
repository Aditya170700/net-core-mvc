using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.DataAccesses.Data;
using Web.DataAccesses.Repository.IRepository;
using Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> results = _unitOfWork.CategoryRepository.GetAll().ToList();

            return View(results);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Store(Category category)
        {
            if (_unitOfWork.CategoryRepository.Any(d => d.Name.ToLower() == category.Name.ToLower())) ModelState.AddModelError("Name", "Category name must be unique");
            if (!ModelState.IsValid) return View("Create", category);

            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Save();

            TempData["success"] = "Category created successfully";

            return RedirectToAction("Index", "Category");
        }

        public IActionResult Edit(int? Id)
        {
            Category? category = _unitOfWork.CategoryRepository.Get(d => d.Id == Id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (_unitOfWork.CategoryRepository.Any(d => d.Name.ToLower() == category.Name.ToLower() && d.Id != category.Id)) ModelState.AddModelError("Name", "Category name must be unique");
            if (!ModelState.IsValid) return View("Edit", category);

            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Save();

            TempData["success"] = "Category updated successfully";

            return RedirectToAction("Index", "Category");
        }

        public IActionResult Delete(int? Id)
        {
            Category? category = _unitOfWork.CategoryRepository.Get(d => d.Id == Id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            if (category == null) return NotFound();

            _unitOfWork.CategoryRepository.Remove(category);
            _unitOfWork.Save();

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index", "Category");
        }
    }
}

