using System;
using TechTalk.SpecFlow;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using Xunit;
using System.Net;

namespace DotNetProject.src.steps
{
    [Binding]
    public class CheckoutSteps
    {
        private readonly ScenarioContext context;

        public CheckoutSteps(ScenarioContext context)
        {
            this.context = context;
        }

         [Given(@"I have taken the order from customer with following order ""(.*)""")]
        public void GivenIHaveTakenTheOrderFromCustomerWithFollowingOrder(string postJsonData){
            var order = JsonConvert.DeserializeObject<Order>(postJsonData);
            var sum = Helpers.CalculateTotal(order.Starters, order.Mains, order.Drinks);
            order.Sum = sum;

            context.Set(order,"Order");
            context.Set(sum,"Total");
        }

        [When(@"I post to checkout API with order details")]
        public async Task WhenIPostToCheckoutAPIWithOrderDetails(){
            var json = JsonConvert.SerializeObject((Order)context.Get<Order>("Order"));
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://postman-echo.com/post";
            using var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic test = JsonConvert.DeserializeObject(result);

            context.Set(response.StatusCode, "ResponseStatusCode");
        }


        [Then(@"the total should be (.*)")]
        public void ThenTheTotalShouldBe(double total){
            Assert.Equal(total, (double)context.Get<double>("Total"));
        }

        [Then(@"response code should be returned ""(.*)""")]
        public void ThenResponseCodeShouldBeReturned(int status){
            Assert.Equal(status, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));
        }

        [Given(@"I have taken the initial order from customer with following order (.*) (.*) (.*) (.*)")]
        public void GivenIHaveTakenTheInitialOrderFromCustomerWithFollowingOrder(int orderId, int starter, int main, int drink){
            var initialOrder = new Order();
            initialOrder.OrderId = orderId;
            initialOrder.Starters = starter;
            initialOrder.Mains = main;
            initialOrder.Drinks = drink;
            var sum = Helpers.CalculateTotal(initialOrder.Starters, initialOrder.Mains, initialOrder.Drinks);
            initialOrder.Sum = sum;

            context.Set(initialOrder,"Order");
            context.Set(sum,"Total");
        }

        [When(@"I updated the same order (.*) (.*) (.*) to checkout")]

        public async Task WhenIUpdatedTheSameOrderToCheckout(int Starters, int Mains, int Drinks){
            var initialOrder = (Order)context.Get<Order>("Order");
            var updateOrder = new Order();
            updateOrder.OrderId = initialOrder.OrderId;
            updateOrder.Starters = initialOrder.Starters + Starters;
            updateOrder.Mains = initialOrder.Mains + Mains;
            updateOrder.Drinks = initialOrder.Drinks + Drinks;
            
            double sum = Helpers.CalculateTotal(updateOrder.Starters,updateOrder.Mains,updateOrder.Drinks);
            updateOrder.Sum = sum;

            context.Set(updateOrder,"Order");
            context.Set(sum,"Total");

            var json = JsonConvert.SerializeObject((Order)context.Get<Order>("Order"));
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://postman-echo.com/post";
            using var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic test = JsonConvert.DeserializeObject(result);

            context.Set(response.StatusCode, "ResponseStatusCode");

        }
    }
}