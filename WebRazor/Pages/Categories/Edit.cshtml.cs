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
	public class EditModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public Category Category { get; set; }

        public EditModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void OnGet(int Id)
        {
            Category = _appDbContext.Categories.Find(Id);
        }

        public IActionResult OnPost()
        {

            if (_appDbContext.Categories.Any(d => d.Name.ToLower() == Category.Name.ToLower() && d.Id != Category.Id)) ModelState.AddModelError("Category.Name", "Category name must be unique");
            if (!ModelState.IsValid) return Page();

            _appDbContext.Categories.Update(Category);
            _appDbContext.SaveChanges();

            TempData["success"] = "Category updated successfully";

            return RedirectToPage("Index");
        }
    }
}
