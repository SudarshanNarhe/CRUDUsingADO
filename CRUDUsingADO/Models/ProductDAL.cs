using System.Data.SqlClient;

namespace CRUDUsingADO.Models
{
    public class ProductDAL
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader dr;

        private readonly IConfiguration configuration;

        public ProductDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration.GetConnectionString("DefaultConnection");
            con = new SqlConnection(constr);
        }

        //GetAllProducts
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string qry = "select * from product";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while (dr.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(dr["productId"]);
                    product.Name = dr["productName"].ToString();
                    product.Price = Convert.ToDouble(dr["price"]);
                    products.Add(product);
                }
            }
           con.Close();
            return products;
        }

        //Add Product
        public int AddProduct(Product product)
        {
            int result = 0;
            string qry = "insert into product values(@productName,@price)";
            cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@productName",product.Name);
            cmd.Parameters.AddWithValue("@price",product.Price);
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        //Edit Product
        public int EditProduct(Product product)
        {
            int result = 0;
            string qry = "update product set productName = @prdName, price = @price where productId = @id";
            cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@prdName", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@id", product.Id);
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        //Delete Product
        public int DeleteProduct(int productId)
        {
            int result = 0;
            string qry = "delete from product where productId = @id";
            cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", productId);
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        //Get A product By Id
        public Product GetProductById(int productId)
        {
            Product product = new Product();
            string qry = "select * from product where productId = @id";
            cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", productId);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    product.Id = Convert.ToInt32(dr["productId"]);
                    product.Name = dr["productName"].ToString();
                    product.Price = Convert.ToDouble(dr["price"]);
                }
            }
            con.Close();
            return product;
        }

    }
}
