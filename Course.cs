using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Project
{
    public partial class Course : Form
    {
        public Course()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0QBBVL3\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True");

        private void Course_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;    
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            string regno = textBox1.Text;

            try
            {
                con.Open();
                SqlCommand comn = new SqlCommand("SELECT * FROM [dbo].[Registration] WHERE (regNo = '" + regno + "')", con);
                SqlDataReader reader;
                reader = comn.ExecuteReader();
                while (reader.Read())
                {
                    textBox2.Text = reader.GetValue(1).ToString();
                    textBox3.Text = reader.GetValue(2).ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
           SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[course]
           ([regNo],[course],[spec])
             VALUES
           ('" + textBox1.Text + "','" + comboBox1.Text + "', '" + comboBox2.Text + "')", con);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Course details submitted successfully");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.Refresh();
                comboBox2.Refresh();
                Application.Exit();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
              if (checkBox1.Checked == false)
              {
                  button2.Enabled = false;
              }
              else
              {
                  button2.Enabled = true;
              }
            
        }
    }
}

