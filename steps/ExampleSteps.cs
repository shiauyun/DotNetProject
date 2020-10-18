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
    public class ExampleSteps
    {
        private readonly ScenarioContext context;

        public ExampleSteps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"I have taken the orders from customer")]
        public void GivenIHaveTakenTheOrdersFromCustomer(){
            Console.WriteLine("Entered orders");
        }

        [When(@"I post to checkout API with following order details ""(.*)""")]
        public async Task WhenIPostToCheckoutAPIWithFollowing(string postJsonData){

            var order = JsonConvert.DeserializeObject<Order>(postJsonData);
            var sum = Helpers.CalculateTotal(order.Starters, order.Mains, order.Drinks);
            
            order.Sum = sum;

            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://postman-echo.com/post";
            using var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic test = JsonConvert.DeserializeObject(result);

            context.Set(response.StatusCode, "ResponseStatusCode");
            context.Set(sum,"Total");
            
        }

        [Then(@"the total should be ""(.*)""")]
        public void ThenTheTotalShouldBe(double total){
            Assert.Equal(total, (double)context.Get<double>("Total"));
        }

        [Then(@"response code should be returned ""(.*)""")]
        public void ThenResponseCodeShouldBeReturned(int status){
            Assert.Equal(status, (int)context.Get<HttpStatusCode>("ResponseStatusCode"));
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

        [When(@"I updated the same order (.*) (.*) (.*) to checkout")]

        public async Task WhenIUpdatedTheSameOrderToCheckout(string Starters, string Mains, string Drinks){
            var initialOrder = (Order)context.Get<Order>("Order");
            var updateOrder = new Order();
            updateOrder.OrderId = initialOrder.OrderId;
            // updateOrder.Starters = initialOrder.Starters + Int32.Parse(Starters);
            // updateOrder.Mains = initialOrder.Mains + Int32.Parse(Mains);
            // updateOrder.Drinks = initialOrder.Drinks + Int32.Parse(Drinks);
            
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