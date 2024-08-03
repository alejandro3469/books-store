using BooksStore.Business.Models;
using BooksStore.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Business
{
    public class BooksStoreBusiness
    {

        public List<GenreModel> GetGenres()
        {
            try
            {
                var booksStoreDataObject = new BooksStoreData();
                var GenresDataTable = booksStoreDataObject.GetGenres();

                var categories = new List<GenreModel>();

                foreach (DataRow item in GenresDataTable.Rows)
                {
                    var category = new GenreModel()
                    {
                        Id = Convert.ToInt32(item["cat_genres_id"]),
                        Name = item["cat_genre_name"].ToString(),
                    };

                    categories.Add(category);
                }

                return categories;
            }
            catch (ApplicationException ex)
            {
                throw;
            }
        }
        public void CreateBook(
            string bookTitle,
            string bookSynopsis,
            string bookImage,
            string bookIsbn,
            double bookPrice,
            List<GenreModel> SelectedGenresList,
            bool bookStatus,
            DateTime bookCreatedAt,
            DateTime bookLastUpdated)
        {
            try
            {
                var datObject = new BooksStoreData();
                datObject.CreateBook(
                    bookTitle,
                    bookSynopsis,
                    bookImage,
                    bookIsbn,
                    bookPrice,
                    SelectedGenresList.Select(genre => genre.Name).ToList(),
                    SelectedGenresList.Select(genre => genre.Id).ToList(),
                    bookStatus,
                    bookCreatedAt,
                    bookLastUpdated);
            }
            catch (ApplicationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error to add book: {ex.Message}");
            }
        }

        public List<BookModel> GetBooks()
        {
            try
            {
                var dataObject = new BooksStoreData();
                var dt = dataObject.GetBooks();

                var books = new List<BookModel>();

                foreach (DataRow item in dt.Rows)
                {
                    var book = new BookModel()
                    {
                        Id = Convert.ToInt32(item["book_id"]),
                        Title = item["book_name"].ToString(),
                        Synopsis = item["book_synopsis"].ToString(),
                        Image = item["book_image"].ToString(),
                        ISBN = item["book_isbn"].ToString(),
                        Price = Convert.ToDouble(item["book_price"]),
                        Status = Convert.ToBoolean(item["book_status"]),
                        CreatedAt = Convert.ToDateTime(item["book_created_at"]),
                        LastUpdated = Convert.ToDateTime(item["book_last_updated"]),
                    };

                    books.Add(book);
                }

                return books;
            }
            catch (ApplicationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error to convert dishes: {ex.Message}");
            }
        }

        public List<string> GetSelectedBookCategories(int bookId)
        {
            try
            {
                var dataObject = new BooksStoreData();
                var dt = dataObject.GetSelectedBookCategories(bookId);

                var categories = new List<string>();

                foreach (DataRow item in dt.Rows)
                {
                    var categoryName = Convert.ToString(item["cat_genre_name"]);

                    categories.Add(categoryName);
                }

                return categories;
            }
            catch (ApplicationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error to convert dishes: {ex.Message}");
            }
        }
    }
}
