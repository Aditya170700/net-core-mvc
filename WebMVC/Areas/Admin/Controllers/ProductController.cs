using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.DataAccesses.Repository.IRepository;
using Web.Models;
using Web.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> results = _unitOfWork.ProductRepository
                .GetAll(includeProperties: "Category")
                .ToList();

            return View(results);
        }

        public IActionResult Upsert(int? Id)
        {
            ProductViewModel vm = new ProductViewModel()
            {
                CategoryList = _unitOfWork.CategoryRepository
                .GetAll()
                .Select(d => new SelectListItem()
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                }),
                Product = _unitOfWork.ProductRepository.Get(d => d.Id == Id) ?? new Product()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel vm, IFormFile? file)
        {
            if (_unitOfWork.ProductRepository.Any(d => d.Id != vm.Product.Id && d.Title.ToLower() == vm.Product.Title.ToLower())) ModelState.AddModelError("Title", "Product title must be unique");
            if (_unitOfWork.ProductRepository.Any(d => d.Id != vm.Product.Id && d.Isbn.ToLower() == vm.Product.Isbn.ToLower())) ModelState.AddModelError("Isbn", "Product ISBN must be unique");
            if (!ModelState.IsValid) return View("Upsert", vm.Product);

            if (file != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images/product");

                if (!string.IsNullOrEmpty(vm.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, vm.Product.ImageUrl.TrimStart('/'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                vm.Product.ImageUrl = @"/images/product/" + fileName;
            }

            string message = "";
            if (new List<int?>() { 0, null }.Contains(vm.Product.Id))
            {
                _unitOfWork.ProductRepository.Add(vm.Product);
                message = "created";
            }
            else
            {
                _unitOfWork.ProductRepository.Update(vm.Product);
                message = "updated";
            }

            _unitOfWork.Save();

            TempData["success"] = $"Product {message} successfully";

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

