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

namespace Menu
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(Properties.Settings.Default.MenuConnString);
                cnn.Open();
                string sqlQuery = "Select * from Users where Username ='" + txtUsername.Text + "'And Password='" + txtPassword.Text + "'";
                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                SqlDataReader dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    MessageBox.Show("Success");
                    Menu form = new Menu(txtUsername.Text);
                    AddDrink add = new AddDrink(txtUsername.Text);
                    AddFood addf = new AddFood(txtUsername.Text);
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    this.Hide();
                    form.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
