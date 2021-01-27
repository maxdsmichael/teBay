using System;

namespace Capstone.Models
{
    public class Bid
    {
        public int Bid_ID { get; set; }
        public int Item_ID { get; set; }
        public int? User_ID { get; set; }
        public decimal Amount { get; set; }
        public string Time_Placed { get; set; }
    }
    public class ReturnBid
    {
        public int Item_ID { get; set; }
        public int Bid_ID { get; set; }
        public string User_Name { get; set; }
        public decimal Amount { get; set; }
        public string Time_Placed { get; set; }
    }
}

