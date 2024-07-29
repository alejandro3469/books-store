using BooksStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Business
{
    public class BooksStoreBusiness
    {
        public void CreateBook(
            string bookName,
            string bookSynopsis,
            string bookImage,
            string bookIsbn,
            double bookPrice,
            bool bookStatus,
            DateTime bookCreatedAt,
            DateTime bookLastUpdated)
        {
            try
            {
                var datObject = new BooksStoreData();
                datObject.CreateBook(
                    bookName,
                    bookSynopsis,
                    bookImage,
                    bookIsbn,
                    bookPrice,
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
