using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WindowsFormsApp7.Models;
using WindowsFormsApp7.Repository.product_order;
using WindowsFormsApp7.Repository.productRep;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        
        
            int UpdId;
            bool read;
            IProductRep productRep;
            IOrderRep orderRep;

            public Form1()
            {
                productRep = new ProductRep("localhost", "SHOP", "root", "root");
                orderRep = new OrderRep("localhost", "SHOP", "root", "root");
                InitializeComponent();
            }

            private void boxesandlables()
            {
                try
                {
                    if (read)
                    {
                        switch (tabControl1.SelectedIndex)
                        {
                            case 0:
                                {
                                    label2.Text = "name:";
                                    label3.Text = "price:";
                                    break;
                                }
                            case 1:
                                {
                                    label2.Text = "client_name:";
                                    label3.Text = "product_id:";
                                    break;
                                }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Err:{ex.Message}");
                }
            }

            private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
            {
                boxesandlables();
            }

            private void textBox_TextChanged(object sender, EventArgs e)
            {
                if ( String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text))
                {

                    button2.Enabled = false;
                    button3.Enabled = false;
                }
                else
                {
                    button2.Enabled = true;
                    button3.Enabled = true;

                }
            }

            private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
            {
            try
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        {
                            UpdId = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                            label4.Text = $"UpdId={UpdId}";
                            if (UpdId > 0)
                            {
                                button3.Enabled = true;
                            }
                            
                            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                            break;
                        }
                    case 1:
                        {
                            UpdId = (int)dataGridView2.SelectedRows[0].Cells[0].Value;
                            label4.Text = $"UpdId={UpdId}";
                            if (UpdId > 0)
                            {
                                button3.Enabled = true;
                            }
                            
                            textBox2.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                            textBox3.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                            break;
                        }
                }
                


            }
            catch (Exception ex)
                {
                    MessageBox.Show($"Err:{ex.Message}");
                }

            }
            private void RefreshTable()
            {
                try
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = productRep.GetAll();
                    dataGridView2.Columns.Clear();
                    dataGridView2.DataSource = orderRep.GetAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Err:{ex.Message}");
                }
            }

            private void button1_Click(object sender, EventArgs e)
            {
                RefreshTable();
                read = true;
                boxesandlables();
            }

            private void button2_Click(object sender, EventArgs e)
            {
                try
                {
                    int rowsaffected = 0;
                    bool correct_id = false;
                    switch (tabControl1.SelectedIndex)
                    {
                        case 0:
                            {
                                rowsaffected = productRep.insert(new product {  price = int.Parse(textBox3.Text), name = textBox2.Text });
                                break;
                            }
                        case 1:
                            {
                                for (int i = 0; i < dataGridView1.RowCount; i++)
                                {
                                    if ((int)dataGridView1.Rows[i].Cells[0].Value == int.Parse(textBox3.Text))
                                    {
                                        rowsaffected = orderRep.insert(new order {  client_name = textBox2.Text, product_id = int.Parse(textBox3.Text) });
                                        correct_id = true;
                                        break;
                                    }
                                }
                                if (!correct_id)
                                {
                                    MessageBox.Show("Неверный product_id");
                                }
                                break;
                            }
                    }
                    MessageBox.Show($"Rows affected: {rowsaffected}");
                    RefreshTable();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Err:{ex.Message}");
                }
            }

            private void button3_Click(object sender, EventArgs e)
            {
                try
                {
                    int rowsaffected = 0;
                    bool correct_id = false;
                    switch (tabControl1.SelectedIndex)
                    {
                        case 0:
                            {
                                rowsaffected = productRep.update(UpdId, new product { price = int.Parse(textBox3.Text), name = textBox2.Text });
                                break;
                            }
                        case 1:
                            {
                                for (int i = 0; i < dataGridView1.RowCount; i++)
                                {
                                
                                    if ((int)dataGridView1.Rows[i].Cells[0].Value == int.Parse(textBox3.Text))
                                    {
                                        rowsaffected = orderRep.update(UpdId, new order { client_name = textBox2.Text, product_id = int.Parse(textBox3.Text) });
                                        correct_id = true;
                                        break;
                                    }
                                }
                                if (!correct_id)
                                {
                                    MessageBox.Show("Неверный product_id");
                                }
                                break;
                            }
                    }
                    MessageBox.Show($"Rows affected: {rowsaffected}");
                    RefreshTable();
                    UpdId = 0;
                    button3.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Err:{ex.Message}");
                }
            }


        }
    

}
