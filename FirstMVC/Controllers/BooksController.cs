using BooksStore.Business;
using BooksStore.Business.Models;
using FirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FirstMVC.Controllers
{
    public class BooksController : Controller
    {

        public static List<BookModel> _books;

        public BooksController()
        {
            var books = new BooksStoreBusiness().GetBooks();

            _books = books;
        }
        // GET: Books
        public ActionResult BooksList()
        {
            ViewBag.genres = new BooksStoreBusiness().GetGenres();

            return View(_books);
        }

        // GET: Books/Details/5
        public ActionResult BookDetails(int id)
        {
            var book = _books.FirstOrDefault(x => x.Id == id);
            ViewBag.selectedGenres = new BooksStoreBusiness().GetSelectedBookCategories(id);
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            var genres = new BooksStoreBusiness().GetGenres();
            ViewBag.Genres = genres;

            return View();
        }

        // POST: Books/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var genres = new BooksStoreBusiness().GetGenres();
                ViewBag.Genres = genres;

                var BooksStoreBusinessObject = new BooksStoreBusiness();
                var genresFullList = BooksStoreBusinessObject.GetGenres();


                var Title = collection["Title"].ToString();
                var ISBN = collection["ISBN"].ToString();
                var Synopsis = collection["Synopsis"].ToString();
                var Image = collection["Image"].ToString();
                var Price = Convert.ToDouble(collection["Price"]);
                var SelectedGenresList = new List<GenreModel>();
                foreach (var genre in genresFullList)
                {
                    if (collection[$"{genre.Name}"] != null)
                    {
                        SelectedGenresList.Add(genre);
                    }
                }
                var Status = collection["Status"] != null;

                var datObject = new BooksStoreBusiness();
                datObject.CreateBook(
                    Title,
                    Synopsis,
                    Image,
                    ISBN,
                    Price,
                    SelectedGenresList,
                    Status,
                    DateTime.Now,
                    DateTime.Now);

                return RedirectToAction("BooksList");
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            var selectedGenres = new BooksStoreBusiness().GetSelectedBookCategories(id);
            var genres = new BooksStoreBusiness().GetGenres();
            ViewBag.selectedGenres = new BooksStoreBusiness().GetSelectedBookCategories(id);
            ViewBag.book = _books.FirstOrDefault(x => x.Id == id);
            ViewBag.Genres = genres;
            return View();
        }

        // POST: Books/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {


                var BooksStoreBusinessObject = new BooksStoreBusiness();
                var genresFullList = BooksStoreBusinessObject.GetGenres();

                ViewBag.selectedGenres = new BooksStoreBusiness().GetSelectedBookCategories(id);


                var Title = collection["Title"].ToString();
                var ISBN = collection["ISBN"].ToString();
                var Synopsis = collection["Synopsis"].ToString();
                var Image = collection["Image"].ToString();
                var Price = Convert.ToDouble(collection["Price"]);
                var SelectedGenresList = new List<GenreModel>();
                foreach (var genre in genresFullList)
                {
                    if (collection[$"{genre.Name}"] != null)
                    {
                        SelectedGenresList.Add(genre);
                    }
                }
                var Status = collection["Status"] != null;

                var datObject = new BooksStoreBusiness();
                datObject.UpdateBook(
                    id,
                    Title,
                    Synopsis,
                    Image,
                    ISBN,
                    Price,
                    SelectedGenresList,
                    Status,
                    DateTime.Now);

                return RedirectToAction("BooksList");
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Books/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
