using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public class FoodItem : MenuItem
    {
        private string foodType;
        private string cuisine;

        public FoodItem() : base()
        {

        }

        // Child class Constructor
        public FoodItem(int id, string itemName, string description, double price, double costPrice, string foodType, string cuisine, string username)
            : base(id, itemName, description, price, costPrice,username)
        {
            this.FoodType = foodType;
            this.Cuisine = cuisine;

        }
        // Getters and Setters
        public string FoodType { get => foodType; set => foodType = value; }
        public string Cuisine { get => cuisine; set => cuisine = value; }

    }

}
