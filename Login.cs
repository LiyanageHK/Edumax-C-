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
using System.Linq.Expressions;
using System.CodeDom;

namespace Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0QBBVL3\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True");
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username, password;

            username = textBox1.Text;
            password = textBox2.Text;

            try
            {
                String querry = "SELECT * FROM login WHERE username= '" + textBox1.Text + "'AND password='" + textBox2.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable login = new DataTable();
                sda.Fill(login);

                if (login.Rows.Count > 0)
                {
                    username = textBox1.Text;
                    password = textBox2.Text;

                    //page that needed to load next 

                    Registration Registration = new Registration();
                    Registration.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login credentials, please check username and password and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();

                    // To focus username

                    textBox1.Focus();

                }
            }
                        catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
                {
                }
            }        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();

            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure? Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true) 
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
