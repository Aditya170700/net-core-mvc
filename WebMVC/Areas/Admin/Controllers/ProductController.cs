using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.DataAccesses.Repository.IRepository;
using Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> results = _unitOfWork.ProductRepository.GetAll().ToList();

            return View(results);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Store(Product product)
        {
            if (_unitOfWork.ProductRepository.Any(d => d.Title.ToLower() == product.Title.ToLower())) ModelState.AddModelError("Title", "Product title must be unique");
            if (_unitOfWork.ProductRepository.Any(d => d.Isbn.ToLower() == product.Isbn.ToLower())) ModelState.AddModelError("Isbn", "Product ISBN must be unique");
            if (!ModelState.IsValid) return View("Create", product);

            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Save();

            TempData["success"] = "Product created successfully";

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Edit(int? Id)
        {
            Product? product = _unitOfWork.ProductRepository.Get(d => d.Id == Id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (_unitOfWork.ProductRepository.Any(d => d.Title.ToLower() == product.Title.ToLower() && d.Id != product.Id)) ModelState.AddModelError("Title", "Product title must be unique");
            if (_unitOfWork.ProductRepository.Any(d => d.Isbn.ToLower() == product.Isbn.ToLower() && d.Id != product.Id)) ModelState.AddModelError("Isbn", "Product ISBN must be unique");
            if (!ModelState.IsValid) return View("Edit", product);

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();

            TempData["success"] = "Product updated successfully";

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Delete(int? Id)
        {
            Product? product = _unitOfWork.ProductRepository.Get(d => d.Id == Id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            if (product == null) return NotFound();

            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted successfully";

            return RedirectToAction("Index", "Product");
        }
    }
}

