using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp7.Models;

namespace WindowsFormsApp7.Repository.productRep
{
    internal class OrderRep : IOrderRep
    {
        private string ConnString { get; set; }
        public OrderRep(string host, string db, string user, string password)
        {
            ConnString = $"server={host};uid={user}; pwd={password};database={db}";
        }
        public List<order> GetAll()
        {
            List<order> table = new List<order>();
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"SELECT*FROM SHOP.product_order;";
            try
            {
                conn.OpenAsync();
                MySqlDataReader reader;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    table.Add(new order { id = reader.GetInt32(0), client_name = reader.GetString(1), product_id = reader.GetInt32(2) });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err: {e.Message}");

            }
            conn.CloseAsync();
            return table;
        }

        public int insert(order value)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"INSERT INTO SHOP.product_order(client_name, product_id) VALUES('{value.client_name}','{value.product_id}');";
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

        public int update(int id, order value)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand com = conn.CreateCommand();
            com.CommandText = $"UPDATE SHOP.product_order SET client_name='{value.client_name}', product_id='{value.product_id}' WHERE id={id};";
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
    }
}
