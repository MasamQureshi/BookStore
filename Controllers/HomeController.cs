using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        // Authors list
        private static List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "Ali", Country = "Pakistan" },
            new Author { Id = 2, Name = "John", Country = "USA" }
        };

        // Books list
        private static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "C# Basics", Price = 500, AuthorId = 1 },
            new Book { Id = 2, Title = "ASP.NET MVC", Price = 800, AuthorId = 2 },
            new Book { Id = 3, Title = "Python", Price = 600, AuthorId = 1 }
        };

        // Home page
        public IActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel();

            vm.Books = new List<BookCardViewModel>();

            foreach (var book in Books)
            {
                string authorName = "";

                foreach (var author in Authors)
                {
                    if (author.Id == book.AuthorId)
                    {
                        authorName = author.Name;
                        break;
                    }
                }

                vm.Books.Add(new BookCardViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    AuthorName = authorName
                });
            }

            vm.Authors = Authors;

            return View(vm);
        }

        // Add Book
        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                int newId = Books.Count == 0 ? 1 : Books.Max(b => b.Id) + 1;

                Books.Add(new Book
                {
                    Id = newId,
                    Title = title,
                    Price = price,
                    AuthorId = authorId
                });
            }

            return RedirectToAction("Index");
        }

        // Add Author
        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            int newId = Authors.Count == 0 ? 1 : Authors.Max(a => a.Id) + 1;

            Authors.Add(new Author
            {
                Id = newId,
                Name = name,
                Country = country
            });

            return RedirectToAction("Index");
        }

        // Remove Book
        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            Book book = Books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                Books.Remove(book);
            }

            return RedirectToAction("Index");
        }

        // About page
        public IActionResult About()
        {
            return View();
        }

        // Contact page
        public IActionResult Contact()
        {
            return View();
        }
    }
}