using System;
using System.Collections;
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
    public partial class AddFood : Form
    {
        int SelectedIndex;
        string username;
        //Creating and initialising a method that compares item list to the created object
        public AddFood(FoodItem pass, int selectedIndex, string username)
        {
            InitializeComponent();
            MenuWorker.selectedFood.Id = pass.Id;
            txtName.Text = pass.ItemName.ToString();
            txtDescrip.Text = pass.Description;
            txtCost.Text = pass.CostPrice.ToString();
            txtSell.Text = pass.Price.ToString();
            txtFoodType.Text = pass.FoodType.ToString();
            txtCuisine.Text = pass.Cuisine.ToString();
            SelectedIndex = selectedIndex;
            this.username = username;
        }
        public AddFood(string username)
        {
            InitializeComponent();
            SelectedIndex = -1;
            this.username = username;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {   // Different ways to clear the textBox
            txtName.Text = String.Empty;
            txtDescrip.Text = String.Empty;
            txtCost.Text = "";
            txtSell.Clear();
            txtCuisine.Clear();
            txtFoodType.Clear();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtDescrip.Text == "" || txtCost.Text == "" || txtSell.Text == "" || txtFoodType.Text == "" || txtCuisine.Text == "")
            {
                MessageBox.Show("Error values Missing!");
            }
            else
            {

                Menu form = new Menu(username);
                if (SelectedIndex == -1)
                {
                    foodAdd();
                }
                else
                {
                    UpdateFood();
                }             
                this.Hide();
                form.ShowDialog();

            }
        }
        //Adding 
        public void foodAdd()
        {
            FoodItem newFood = new FoodItem(0, txtName.Text, txtDescrip.Text, Convert.ToDouble(txtSell.Text), Convert.ToDouble(txtCost.Text), txtFoodType.Text, txtCuisine.Text, username);
            try
            {
                SqlConnection cnn = new SqlConnection(Properties.Settings.Default.MenuConnString);
                cnn.Open();

                string sqlQuery = "Insert into Food_Item(ItemName,Description,Price,CostPrice,FoodType,Cuisine,Username)VALUES ('"
                    + newFood.ItemName + "','" + newFood.Description + "','" + newFood.Price + "','" + newFood.CostPrice
                    + "','" + newFood.FoodType + "','" + newFood.Cuisine + "','" +newFood.UserName+ "');SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;

                int id = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());
                newFood.Id = id;
                newFood.UserName = username;
                MenuWorker.foodList.Add(newFood);

                adapter.Dispose();
                command.Dispose();
                cnn.Close();
                MessageBox.Show("Food Added Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Editing
        public void UpdateFood()
        {
            FoodItem newFood = new FoodItem(MenuWorker.selectedFood.Id, txtName.Text, txtDescrip.Text, Convert.ToDouble(txtSell.Text), Convert.ToDouble(txtCost.Text),txtFoodType.Text, txtCuisine.Text, username);
            try
            {
                SqlConnection cnn = new SqlConnection(Properties.Settings.Default.MenuConnString);
                cnn.Open();

                string sqlQuery = "Update Food_Item set ItemName='" + newFood.ItemName
                    + "',Description='" + newFood.Description + "',Price='" + newFood.Price
                    + "',CostPrice='" + newFood.CostPrice + "',FoodType='" + newFood.FoodType
                    + "',Cuisine='" + newFood.Cuisine + "'WHERE ID=" + newFood.Id;


                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.ExecuteNonQuery();


                MessageBox.Show("Food Updated Successfully");
                MenuWorker.foodList.Remove(MenuWorker.selectedFood);
                MenuWorker.foodList.Add(newFood);

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
