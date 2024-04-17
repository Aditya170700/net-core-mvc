using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Data;
using WebRazor.Models;

namespace WebRazor.Pages.Categories
{
    [BindProperties]
	public class CreateModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public Category Category { get; set; }

        public CreateModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (_appDbContext.Categories.Any(d => d.Name.ToLower() == Category.Name.ToLower())) ModelState.AddModelError("Name", "Category name must be unique");
            if (!ModelState.IsValid) return RedirectToPage("Create");

            _appDbContext.Categories.Add(Category);
            _appDbContext.SaveChanges();

            TempData["success"] = "Category created successfully";

            return RedirectToPage("Index");
        }
    }
}
