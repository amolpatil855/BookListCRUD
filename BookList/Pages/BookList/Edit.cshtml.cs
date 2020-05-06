using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }
        public async Task OnGet(int id)
        {
            Book =await _db.Book.FindAsync(id);
        }

        public async Task<IActionResult> OnPost(Book book)
        {
            if (ModelState.IsValid)
            {
                var oldBook = await _db.Book.FindAsync(book.Id);
                oldBook.Name = book.Name;
                oldBook.Author = book.Author;
                oldBook.ISBN = book.ISBN;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }

    }
}