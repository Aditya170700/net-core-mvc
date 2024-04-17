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
	public class DeleteModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public Category Category { get; set; }

        public DeleteModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void OnGet(int Id)
        {
            Category = _appDbContext.Categories.Find(Id);
        }

        public IActionResult OnPost()
        {
            if (Category == null) return NotFound();

            _appDbContext.Categories.Remove(Category);
            _appDbContext.SaveChanges();

            TempData["success"] = "Category deleted successfully";

            return RedirectToPage("Index");
        }
    }
}
