using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class MenuWorker
    {
        // Declaring of Lists of objects
        public static List<DrinkItem> drinkList = new List<DrinkItem>();
        public static List<FoodItem> foodList = new List<FoodItem>();
        public static List<DrinkItem> filtered;
        public static List<FoodItem> filtered1;
        public static FoodItem selectedFood = new FoodItem();
        public static DrinkItem selectedDrink = new DrinkItem();

    }
}
