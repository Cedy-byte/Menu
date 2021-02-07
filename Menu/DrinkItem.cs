using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public class DrinkItem : MenuItem
    {

        private string container;
        private string drinkType;

        public DrinkItem() : base()
        {

        }

        // Child class Constructor
        public DrinkItem(int id, string itemName, string description, double price, double costPrice, string container, string drinkType, string username)
        : base(id, itemName, description, price, costPrice,username)
        {

            this.Container = container;
            this.DrinkType = drinkType;
        }

        // Getters and Setters       
        public string Container { get => container; set => container = value; }
        public string DrinkType { get => drinkType; set => drinkType = value; }
    }

}
