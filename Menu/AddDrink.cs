using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu
{
    public partial class AddDrink : Form
    {
        int SelectedIndex;
        string username;
        public AddDrink(DrinkItem pass, int selectedIndex, string username)
        {
            InitializeComponent();
            MenuWorker.selectedDrink.Id = pass.Id;
            txtName.Text = pass.ItemName.ToString();
            txtDescrip.Text = pass.Description;
            txtCost.Text = pass.CostPrice.ToString();
            txtSell.Text = pass.Price.ToString();
            txtContType.Text = pass.Container.ToString();
            txtDrinkType.Text = pass.DrinkType.ToString();
            SelectedIndex = selectedIndex;
            this.username = username;
            

        }

        public AddDrink(string username)
        {
            InitializeComponent();
            SelectedIndex = -1;
            this.username = username;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = String.Empty;
            txtDescrip.Text = String.Empty;
            txtCost.Text = "";
            txtSell.Clear();
            txtContType.Clear();
            txtDrinkType.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtDescrip.Text == "" || txtCost.Text == "" || txtSell.Text == "" || txtContType.Text == "" || txtDrinkType.Text == "")
            {
                MessageBox.Show("Error values Missing!");
            }
            else
            {
                Menu form = new Menu(username);           
                if (SelectedIndex == -1)
                {
                    addDrrink();
                }
                else
                {
                    UpdateDrink();
                }               
                this.Hide();
                form.ShowDialog();
            }


        }
        //Adding
        public void addDrrink()
        {
            DrinkItem newDrink = new DrinkItem(0, txtName.Text, txtDescrip.Text, Convert.ToDouble(txtSell.Text), Convert.ToDouble(txtCost.Text),txtContType.Text, txtDrinkType.Text, username);
            try
            {
                SqlConnection cnn = new SqlConnection(Properties.Settings.Default.MenuConnString);
                cnn.Open();

                string sqlQuery = "Insert into Drink_Item(ItemName,Description,Price,CostPrice,Container,DrinkType,Username)VALUES ('"
                    + newDrink.ItemName + "','" + newDrink.Description + "','" + newDrink.Price + "','" + newDrink.CostPrice
                    + "','" + newDrink.Container + "','" + newDrink.DrinkType + "','" + newDrink.UserName+ "');SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;

                int id = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());              
                newDrink.Id = id;
                newDrink.UserName = username;

                MenuWorker.drinkList.Add(newDrink);

                adapter.Dispose();
                command.Dispose();
                cnn.Close();
                MessageBox.Show("Drink Added Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Editing
        public void UpdateDrink()
        {
            DrinkItem newDrink = new DrinkItem(MenuWorker.selectedDrink.Id, txtName.Text, txtDescrip.Text, Convert.ToDouble(txtSell.Text), Convert.ToDouble(txtCost.Text),txtContType.Text, txtDrinkType.Text, username);
            try
            {
                SqlConnection cnn = new SqlConnection(Properties.Settings.Default.MenuConnString);
                cnn.Open();

                string sqlQuery = "Update Drink_Item set ItemName='" + newDrink.ItemName
                    + "',Description='" + newDrink.Description + "',Price='" + newDrink.Price
                    + "',CostPrice='" + newDrink.CostPrice + "',Container='" + newDrink.Container
                    + "',DrinkType='" + newDrink.DrinkType + "'WHERE ID=" + newDrink.Id;


                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.ExecuteNonQuery();


                MessageBox.Show("Drink Updated Successfully");
                MenuWorker.drinkList.Remove(MenuWorker.selectedDrink);
                MenuWorker.drinkList.Add(newDrink);

                adapter.Dispose();
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
