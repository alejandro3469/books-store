using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Data
{
    public class BooksStoreData
    {
        private readonly string ConnectionString;
        public BooksStoreData()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
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
                var connection = new SqlConnection(ConnectionString);
                var command = new SqlCommand("spCreateBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter() { ParameterName = "book_name", DbType = DbType.String, Value = bookName });
                command.Parameters.Add(new SqlParameter() { ParameterName = "book_synopsis", DbType = DbType.String, Value = bookSynopsis });
                command.Parameters.Add(new SqlParameter() { ParameterName = "book_image", DbType = DbType.Int32, Value = bookImage });
                command.Parameters.Add(new SqlParameter() { ParameterName = "book_isbn", DbType = DbType.Boolean, Value = bookIsbn });
                command.Parameters.Add(new SqlParameter() { ParameterName = "book_price", DbType = DbType.Int32, Value = bookPrice });
                command.Parameters.Add(new SqlParameter() { ParameterName = "book_status", DbType = DbType.String, Value = bookStatus });
                command.Parameters.Add(new SqlParameter() { ParameterName = "book_created_at", DbType = DbType.DateTime, Value = bookCreatedAt });
                command.Parameters.Add(new SqlParameter() { ParameterName = "book_last_updated", DbType = DbType.DateTime, Value = bookLastUpdated });

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                var script = $"alert('The book {bookName} was added successfully')";
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error to add book, id: : {ex.Message}");
            }
        }
    }
}
