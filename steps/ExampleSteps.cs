using System;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace DotNetProject.src.steps
{
    [Binding]
    public class ExampleSteps
    {
        [Given(@"I have entered the orders from customer")]
        public void GivenEnteredOrders(){
            Console.WriteLine("Entered orders");
        }

        [When(@"I press checkout")]
        public void WhenCheckout(){
            Console.WriteLine("Checkout");
        }

        [Then(@"the result should be the total of all orders")]
        public void ThenTotal(){
            Console.WriteLine("Total");
        }

        [And(@"response code should be returned 200 OK")]
        public void AndResponse(){
            Console.WriteLine("repsonse code 200");
        }
    }
}