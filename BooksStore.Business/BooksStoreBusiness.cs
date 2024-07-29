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
    }
}
