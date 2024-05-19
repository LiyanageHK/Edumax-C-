using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0QBBVL3\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login obj = new Login();
            var result = MessageBox.Show("Are you sure, Do you really want to logout?", "logout", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                obj.Show();
                this.Close();
            }
            else if (result == DialogResult.Yes)
            {
                this.Close();
            }
            

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure, Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                DialogResult result = MessageBox.Show("Are you sure, Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    SqlCommand del = new SqlCommand("DELETE FROM [dbo].[Registration] WHERE regNo = '" + textBox1.Text + "'", con);
                    del.ExecuteNonQuery();
                    MessageBox.Show("Record deleted successfully");
                }
                else
                {
                    Registration Registration = new Registration();
                    Registration.Show();
                    this.Hide();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
            finally
            {
                con.Close();
            }
            try
            {
                con.Open();
                SqlCommand del = new SqlCommand("DELETE FROM [dbo].[course] WHERE regNo = '" + textBox1.Text + "'", con);
                del.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            con.Open();
           
            SqlCommand commToCheckregNo = new SqlCommand("SELECT regNo from Registration where regNo='" + int.Parse(textBox1.Text) + "'", con);
            SqlDataAdapter sd=new SqlDataAdapter(commToCheckregNo);
            DataTable dt= new DataTable();
            sd.Fill(dt);
            con.Close();
            if (dt.Rows.Count>0)
            {
                MessageBox.Show("Record already exists");
            }
            else
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Registration]
           ([regNo]
           ,[firstName]
           ,[lastName]
           ,[dateOfBirth]
           ,[gender]
           ,[Address]
           ,[email]
           ,[mobilePhone]
           ,[homePhone]
           ,[parentName]
           ,[nic]
           ,[contactNo])
             VALUES
           ('" + textBox1.Text + "','" + textBox2.Text + "', '" + textBox3.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + comboBox1.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "'," +
           " '" + textBox8.Text + "', '" + textBox9.Text + "', '" + textBox10.Text + "')", con);
                
                if (con.State==ConnectionState.Closed)
                {
                    con.Open();
                }
             
                cmd.ExecuteNonQuery();
                con.Close();
                DialogResult result = MessageBox.Show("Do you want to register?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Record added successfully");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    dateTimePicker1.Refresh();
                    comboBox1.Refresh();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    textBox9.Clear();
                    textBox10.Clear();

                    Course Course = new Course();
                    Course.Show();
                    this.Hide();
                }
                else
                {
                    Registration Registration = new Registration();
                    Registration.Show();
                    this.Hide();
                }
                

                
            }
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime from = dateTimePicker1.Value;
            DateTime to = DateTime.Now;
            TimeSpan TSpan = to - from;
            Double day = TSpan.TotalDays;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Text = string.Empty;
            comboBox1.Refresh();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "UPDATE Registration SET  firstName = '" + textBox2.Text + "', lastName = '" + textBox3.Text + "', dateOfBirth = '" + DateTime.Now.ToString("yyyy-MM-dd") + 
                "', gender = '" + comboBox1.Text + "', address = '" + textBox4.Text + "', email = '" + textBox5.Text + "', mobilePhone = '" + textBox6.Text + "', homePhone = '" + textBox7.Text + 
                "', parentName = '" + textBox8.Text + "', nic = '" + textBox9.Text + "', contactNo = '" + textBox10.Text + "' WHERE  regNo = '"+ textBox1.Text + "' ";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.ExecuteNonQuery();
                MessageBox.Show("Record updated successfully");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
            finally
            {
                con.Close();
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string regno = textBox11.Text;

            try
            {
                con.Open();
                SqlCommand comn = new SqlCommand("SELECT * FROM [dbo].[Registration] WHERE (regNo = '"+ regno + "')", con);
                SqlDataReader reader;
                reader = comn.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader.GetValue(0).ToString();
                    textBox2.Text = reader.GetValue(1).ToString();
                    textBox3.Text = reader.GetValue(2).ToString();
                    dateTimePicker1.Text = reader.GetValue(3).ToString();
                    comboBox1.Text = reader.GetValue(4).ToString();
                    textBox4.Text = reader.GetValue(5).ToString();
                    textBox5.Text = reader.GetValue(6).ToString();
                    textBox6.Text = reader.GetValue(7).ToString();
                    textBox7.Text = reader.GetValue(8).ToString();
                    textBox8.Text = reader.GetValue(9).ToString();
                    textBox9.Text = reader.GetValue(10).ToString();
                    textBox10.Text = reader.GetValue(11).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR in searching" + ex);
            }
            finally
            {
                con.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Registration_Load(object sender, EventArgs e)
        {
            
        }
    }
}
