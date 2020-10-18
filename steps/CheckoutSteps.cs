using System;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
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
        }

        [When(@"I post to checkout API with order details")]
        public async Task WhenIPostToCheckoutAPIWithOrderDetails(){
            var json = JsonConvert.SerializeObject((Order)context.Get<Order>("Order"));
            
            var resp = await Helpers.PostOrder(json);
            string res = resp.Content.ReadAsStringAsync().Result;
            dynamic test = JsonConvert.DeserializeObject(res);
            context.Set(resp.StatusCode, "ResponseStatusCode"); 
            context.Set((double)test.data.Sum, "FinalSumReturned");
            context.Set((double)test.data.Sum, "InitialSumReturned");
        }


        [Then(@"the final total should be (.*)")]
        public void ThenTheFinalTotalShouldBe(double total){
            Assert.Equal(total, context.Get<double>("FinalSumReturned"));
        }

        [Then(@"response code should return ""(.*)""")]
        public void ThenResponseCodeShouldReturn(int status){
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

            var json = JsonConvert.SerializeObject((Order)context.Get<Order>("Order"));
            var resp = await Helpers.PostOrder(json);
            string res = resp.Content.ReadAsStringAsync().Result;
            dynamic test = JsonConvert.DeserializeObject(res);
            context.Set(resp.StatusCode, "ResponseStatusCode");
            context.Set((double)test.data.Sum, "FinalSumReturned");
        }

        [Then(@"the initial total should be (.*)")]
        public void ThenTheInitialTotalShouldBe(double total){
            Assert.Equal(total, context.Get<double>("InitialSumReturned"));
        }
    }
}