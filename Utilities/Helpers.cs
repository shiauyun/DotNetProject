using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

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

        public static async Task<HttpResponseMessage> PostOrder(string jsonOrderData){

            var data = new StringContent(jsonOrderData, Encoding.UTF8, "application/json");

            var url = "https://postman-echo.com/post";
            using var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, data);
            return response;

        }
    }
}