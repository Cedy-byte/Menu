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
    public partial class Menu : Form
    {
        //public FoodItem selectedFood = new FoodItem();
        string username;
        public Menu(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        public Menu()
        {
            InitializeComponent();
            
        }

        // Validating which item needs to be added to the Menu
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (radFood.Checked)
            {
                this.Hide();
                AddFood add1 = new AddFood(username);
                add1.ShowDialog();
                
            }
            else if (radDrink.Checked)
            {
                this.Hide();
                AddDrink add2 = new AddDrink(username);
                add2.ShowDialog();
                
            }
            else if (radDrink.Checked == false && radFood.Checked == false)
            {
                MessageBox.Show("Please Select the item you would like to Add !");
            }


        }

        private void MenuForm1_Load(object sender, EventArgs e)
        {
            ReadFromFood();
            ReadFromDrink();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbMeals.SelectedItem != null || lbDrinks.SelectedItem != null)
            {
                deleteFood();
                deleteDrink();
            }
            else
            {
                MessageBox.Show("Please Select an Item you Wish to delete");
            }
        }

        // Passing the line to be updated to the respective form
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lbMeals.SelectedItem != null || lbDrinks.SelectedItem != null)
            {
                try
                {

                    for (int i = 0; i < MenuWorker.foodList.Count; i++)
                    {
                        if (lbMeals.SelectedIndex == i)
                        {
                            FoodItem foodItem = new FoodItem(MenuWorker.foodList[i].Id, MenuWorker.foodList[i].ItemName, MenuWorker.foodList[i].Description,
                                MenuWorker.foodList[i].Price, MenuWorker.foodList[i].CostPrice, MenuWorker.foodList[i].FoodType,
                                MenuWorker.foodList[i].Cuisine, MenuWorker.foodList[i].UserName);

                            this.Hide();
                            AddFood add = new AddFood(foodItem, lbMeals.SelectedIndex, username);
                            add.ShowDialog();
                            
                            


                        }
                    }
                    for (int i = 0; i < MenuWorker.drinkList.Count; i++)
                    {
                        if (lbDrinks.SelectedIndex == i)
                        {
                            DrinkItem drinkItem = new DrinkItem(MenuWorker.drinkList[i].Id, MenuWorker.drinkList[i].ItemName, MenuWorker.drinkList[i].Description,
                                MenuWorker.drinkList[i].Price, MenuWorker.drinkList[i].CostPrice, MenuWorker.drinkList[i].Container,
                                MenuWorker.drinkList[i].DrinkType, MenuWorker.drinkList[i].UserName);
          
                            this.Hide();
                            AddDrink add = new AddDrink(drinkItem, lbDrinks.SelectedIndex, username);
                            add.ShowDialog();
                            


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
            else
            {
                MessageBox.Show("Please Select an item you Wish to Update from the List");
            }
        }

        // Reading from the food text file and displaying each line in the listbox
        public void ReadFromFood()
        {
            MenuWorker.foodList.Clear();
            try
            {
                SqlConnection cnn = new SqlConnection(Properties.Settings.Default.MenuConnString);
                cnn.Open();
                string sqlQuery = "Select * from Food_Item WHERE Username ='" + username + "'";
                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    FoodItem temp = new FoodItem(dataReader.GetInt32(0), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(),
                        dataReader.GetDouble(3), dataReader.GetDouble(4), dataReader.GetValue(5).ToString(), dataReader.GetValue(6).ToString(),
                        dataReader.GetValue(7).ToString());
                    MenuWorker.foodList.Add(temp);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            lbMeals.Items.Clear();
            foreach (FoodItem food in MenuWorker.foodList)
            {
                lbMeals.Items.Add(food.ItemName + " | " + food.Description + " |" + " ............................................. R" + food.Price);
            }
        }

        // Reading from the Drink text file and displaying each line in the listbox
        public void ReadFromDrink()
        {
            MenuWorker.drinkList.Clear();
            try
            {
                SqlConnection cnn = new SqlConnection(Properties.Settings.Default.MenuConnString);
                cnn.Open();
                string sqlQuery = "Select * from Drink_Item WHERE Username ='" + username + "'";
                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    DrinkItem temp = new DrinkItem(dataReader.GetInt32(0), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(),
                        dataReader.GetDouble(3), dataReader.GetDouble(4), dataReader.GetValue(5).ToString(), dataReader.GetValue(6).ToString(),
                        dataReader.GetValue(7).ToString());
                    MenuWorker.drinkList.Add(temp);
                }
                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            lbDrinks.Items.Clear();
            foreach (DrinkItem drink in MenuWorker.drinkList)
            {
                lbDrinks.Items.Add(drink.ItemName + " | " + drink.Description + " |" + " ............................................. R" + drink.Price);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lbDisplay.Items.Clear();
            lblSearch.Visible = false;
            txtSearch.Visible = false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            lbDisplay.Items.Clear();
            if (cmbDisplay.SelectedItem != null)
            {
                string option = cmbDisplay.SelectedItem.ToString();

                switch (option)
                {
                    case "View the menu in ascending order of Price":

                        MenuWorker.filtered = MenuWorker.drinkList.OrderBy(f => f.Price).ToList();
                        MenuWorker.filtered1 = MenuWorker.foodList.OrderBy(fd => fd.Price).ToList();


                        break;

                    case "View budget items":
                        MenuWorker.filtered = MenuWorker.drinkList.Where(f => f.Price < 25).OrderBy(fn => fn.Price).ToList();
                        MenuWorker.filtered1 = MenuWorker.foodList.Where(fd => fd.Price < 25).OrderBy(fnd => fnd.Price).ToList();


                        break;

                    case "Premium menu items":
                        MenuWorker.filtered = MenuWorker.drinkList.Where(f => f.Price > 55).OrderBy(fn => fn.Price).ToList();
                        MenuWorker.filtered1 = MenuWorker.foodList.Where(fd => fd.Price > 55).OrderBy(fnd => fnd.Price).ToList();


                        break;

                    case "View only the non-alcoholic beverages":

                        MenuWorker.filtered = MenuWorker.drinkList.Where(f => f.DrinkType != "Alcoholic").OrderBy(fn => fn.ItemName).ToList();
                        MenuWorker.filtered1 = MenuWorker.foodList.Where(fd => fd.ItemName.Equals("Alcoholic")).ToList();//fix this


                        break;

                    case "Search for An item":
                        if (txtSearch.Text != null)
                        {
                            MenuWorker.filtered = MenuWorker.drinkList.Where(f => f.ItemName.Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                            MenuWorker.filtered1 = MenuWorker.foodList.Where(fd => fd.ItemName.Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                        }
                        break;
                }
                foreach (DrinkItem f in MenuWorker.filtered)
                {
                    lbDisplay.Items.Add(f.ToString());

                }
                foreach (FoodItem fd in MenuWorker.filtered1)
                {
                    lbDisplay.Items.Add(fd.ToString());
                }

            }
            else
            {
                MessageBox.Show("Please Select an option to View details");
            }
        }

        private void cmbDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDisplay.SelectedItem.ToString() == "Search for An item")
            {
                lblSearch.Visible = true;
                txtSearch.Visible = true;
            }
            else
            {
                lblSearch.Visible = false;
                txtSearch.Visible = false;
            }
        }

        public void deleteDrink()
        {
            for (int i = 0; i < MenuWorker.drinkList.Count; i++)
            {
                if (lbDrinks.SelectedIndex == i)
                {
                    DrinkItem drinkItem = new DrinkItem(MenuWorker.drinkList[i].Id, MenuWorker.drinkList[i].ItemName, MenuWorker.drinkList[i].Description,
                       MenuWorker.drinkList[i].Price, MenuWorker.drinkList[i].CostPrice, MenuWorker.drinkList[i].Container,
                       MenuWorker.drinkList[i].DrinkType, MenuWorker.drinkList[i].UserName);

                    try
                    {
                        SqlConnection cnn = new SqlConnection(Properties.Settings.Default.MenuConnString);
                        cnn.Open();

                        string sqlQuery = "Delete from Drink_Item WHERE ID= " + drinkItem.Id;

                        SqlCommand command = new SqlCommand(sqlQuery, cnn);
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.DeleteCommand = command;
                        adapter.DeleteCommand.ExecuteNonQuery();


                        MessageBox.Show("Drink Deleted Successfully");
                        MenuWorker.drinkList.Remove(drinkItem);
                        lbDrinks.Items.Remove(lbDrinks.SelectedItem);

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

        public void deleteFood()
        {
            for (int i = 0; i < MenuWorker.foodList.Count; i++)
            {
                if (lbMeals.SelectedIndex == i)
                {
                    FoodItem foodItem = new FoodItem(MenuWorker.foodList[i].Id, MenuWorker.foodList[i].ItemName, MenuWorker.foodList[i].Description,
                        MenuWorker.foodList[i].Price, MenuWorker.foodList[i].CostPrice, MenuWorker.foodList[i].FoodType,
                        MenuWorker.foodList[i].Cuisine, MenuWorker.foodList[i].UserName);

                    try
                    {
                        SqlConnection cnn = new SqlConnection(Properties.Settings.Default.MenuConnString);
                        cnn.Open();

                        string sqlQuery = "Delete from Food_Item WHERE ID= " + foodItem.Id;

                        SqlCommand command = new SqlCommand(sqlQuery, cnn);
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.DeleteCommand = command;
                        adapter.DeleteCommand.ExecuteNonQuery();


                        MessageBox.Show("Food Deleted Successfully");
                        MenuWorker.foodList.Remove(foodItem);
                        lbMeals.Items.Remove(lbMeals.SelectedItem);

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
       

    }
}
