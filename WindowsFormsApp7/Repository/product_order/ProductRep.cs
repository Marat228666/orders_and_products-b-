using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp7.Models;
using MySql.Data;
using MySql.Web;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp7.Repository.product_order
{
    internal class ProductRep : IProductRep
    {
        private string ConnString { get; set; }
        public ProductRep(string host, string db, string user, string password)
        {
            ConnString = $"server={host};uid={user}; pwd={password};database={db}";
        }
        public List<product> GetAll()
        {
            List<product> table = new List<product>();
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"SELECT*FROM SHOP.product;";
            try
            {
                conn.OpenAsync();
                MySqlDataReader reader;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    table.Add(new product { id = reader.GetInt32(0), name = reader.GetString(1), price = reader.GetInt32(2) });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err: {e.Message}");

            }
            conn.CloseAsync();
            return table;
        }

        public int insert(product value)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"INSERT INTO SHOP.product(name, price) VALUES('{value.name}','{value.price}');";
            try
            {
                conn.OpenAsync();
                rows = com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err: {e.Message}");

            }
            conn.CloseAsync();
            return rows;
        }

        public int update(int id, product value)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"UPDATE SHOP.product SET name='{value.name}', price='{value.price}' WHERE id={id};";
            try
            {
                conn.OpenAsync();
                rows = com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err: {e.Message}");

            }
            conn.CloseAsync();
            return rows;
        }

        public int Delete(int id)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = $"DELETE FROM SHOP.product WHERE id={id};";
            try
            {
                conn.OpenAsync();
                rows = comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err:{e.Message}");
            }
            conn.CloseAsync();
            return rows;
        }
    }

}

