using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookList.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        //dependecy injection
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var bookToDelete = await _db.Book.FindAsync(id);
            if (bookToDelete != null)
            {
                _db.Book.Remove(bookToDelete);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}