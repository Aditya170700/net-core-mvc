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
	public class IndexModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public List<Category> Categories { get; set; }

        public IndexModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void OnGet()
        {
            Categories = _appDbContext.Categories.ToList();
        }
    }
}
