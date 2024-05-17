using System.Data.SqlClient;

namespace CRUDUsingADO.Models
{
    public class BookDAL
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader reader;

        private readonly IConfiguration config;

        public BookDAL(IConfiguration config)
        {
            this.config = config;
            string connect = config.GetConnectionString("DefaultConnection");
            con = new SqlConnection(connect);
        }

        //Get List Of Books
        public List<Book> GetBooks()
        {
            List<Book> booklist = new List<Book>();
            string qry = "select * from book";
            cmd = new SqlCommand(qry, con);
            con.Open();
            reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(reader["id"]);
                    book.BookName = reader["bookName"].ToString();
                    book.Author = reader["author"].ToString();
                    book.Price = Convert.ToDouble(reader["price"]);
                    booklist.Add(book);
                }
            }
            con.Close();
            return booklist;
        }

        //add book
        public int AddBook(Book book)
        {
            int result = 0;
            string qry = "insert into book values(@bookName,@author,@price)";
            cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@bookName", book.BookName);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@price",book.Price);
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        //update book

        public int EditBook(Book book)
        {
            int result = 0;
            string qry = "update book set bookName=@bookName, author = @author, price =@price where id = @id";
            cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@bookName", book.BookName);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@price", book.Price);
            cmd.Parameters.AddWithValue("@id", book.Id);
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        //Get a book by id
        public Book GetBookById(int id)
        {   
            Book book = new Book();
            string qry = "select * from book where id = @id";
            cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    book.Id = Convert.ToInt32(reader["id"]);
                    book.BookName = reader["bookName"].ToString();
                    book.Author = reader["author"].ToString();
                    book.Price = Convert.ToDouble(reader["price"]);
                }
            }
            con.Close();
            return book;
        }

        //delete book
        public int DeleteBook(int id)
        {
            int result = 0;
            string qry = "delete from book where id = @id";
            cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
