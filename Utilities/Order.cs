using System;

namespace DotNetProject
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Starters { get; set; }
        public int Mains { get; set; }
        public int Drinks { get; set; }
        public double Sum { get; set; }
    }
}