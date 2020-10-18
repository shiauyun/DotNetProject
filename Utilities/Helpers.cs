using System;

namespace DotNetProject
{
    public static class Helpers
    {
        public const double starterCost = 4;
        public const double mainCost = 7;
        public const double drinkCost = 2.5;

        public static double CalculateTotal(int startersQty, int mainQty, int drinkQty){
            double sumFood = (((starterCost * startersQty)+(mainCost * mainQty))*1.1);
            double total = Math.Round((sumFood+(drinkCost * drinkQty)),2);
            return total;
        }
    }
}