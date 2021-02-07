using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public abstract class MenuItem : IComparable
    {
        // Variable Declaration
        private int id;
        private string itemName;
        private string description;
        private double price;
        private double costPrice;
        private string userName;


        // Parent class constructor
        public MenuItem(int id, string itemName, string description, double price, double costPrice, string userName)
        {
            this.Id = id;
            this.ItemName = itemName;
            this.Description = description;
            this.Price = price;
            this.CostPrice = costPrice;
            this.UserName = userName;
        }

        public MenuItem()
        {
        }

        // Getters and Setters
        public int Id { get => id; set => id = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public string Description { get => description; set => description = value; }
        public double Price { get => price; set => price = value; }
        public double CostPrice { get => costPrice; set => costPrice = value; }
        public string UserName { get => userName; set => userName = value; }

        public override string ToString()
        {
            string output;
            if (this.ItemName.Length < 9)
                output = this.ItemName + "\t\t\t" + this.Description + "\t\t R" + this.Price;
            else
                output = this.ItemName + "\t\t" + this.Description + "\t\t R" + this.Price;

            return output;
        }

        public int CompareTo(object obj)
        {
            return itemName.CompareTo(obj.ToString());
        }


    }


}
