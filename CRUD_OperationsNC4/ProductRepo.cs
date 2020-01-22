using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace CRUD_OperationsNC4
{
    public class ProductRepo
    {
        // READ - GetProducts()
        //HOW TO HIDE CONNECTION STRING
        //public string connString = "Server=localhost;Database=bestbuy;Uid=root;Pwd=password;";
        public static string connString;

        public List<Product> GetProducts()
        {
            MySqlConnection conn = new MySqlConnection(connString);
            using (conn)
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();

                command.CommandText = "SELECT ProductID, Name, Price, StockLevel FROM products;";
                MySqlDataReader reader = command.ExecuteReader();

                var products = new List<Product>();

                while (reader.Read())
                {
                    var row = new Product();
                    row.ProductID = reader.GetInt32("ProductID");
                    row.Name = reader.GetString("Name");
                    row.Price = reader.GetInt32("Price");
                    row.StockLevel = reader.GetInt32("StockLevel");

                    products.Add(row);
                }
                return products;
            }

        }

        //CREATE
        public void CreateProduct(Product p)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using(conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO products(Name, Price, CategoryID, OnSale ) " +
                    "VALUES (@name, @price, @catID, @sale);";

                cmd.Parameters.AddWithValue("name", p.Name);
                cmd.Parameters.AddWithValue("price", p.Price);
                cmd.Parameters.AddWithValue("catID", p.CategoryID);
                cmd.Parameters.AddWithValue("sale", p.OnSale);
                cmd.ExecuteNonQuery();
            }
        }

      
        // UPDATE

        public void UpdateProduct(Product p)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET StockLevel=@stock WHERE ProductID=@prodID;";
                cmd.Parameters.AddWithValue("stock", p.StockLevel);
                cmd.Parameters.AddWithValue("prodID", p.ProductID);
                cmd.ExecuteNonQuery();
            }
        }

        //DELETE by ProductID
        public void DeleteProductID(int ID)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE ProductID=@ID;";
                cmd.Parameters.AddWithValue("ID", ID);
                cmd.ExecuteNonQuery();
            }
        }

        //DELETE by Name
        public void DeleteProductName(string name)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE Name=@Name;";
                cmd.Parameters.AddWithValue("Name", name);
                cmd.ExecuteNonQuery();
            }
        }

        //DELETE by Name & ID
        public void DeleteProductNameID(string name, int id)
        {
            MySqlConnection conn = new MySqlConnection(connString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE Name=@name AND ProductID=@id;";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("ID", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
