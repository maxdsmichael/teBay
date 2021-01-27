using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Item
    {
        public int Item_ID { get; set; }
        public int Auction_ID { get; set; }
        public string Donor { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Pic { get; set; }
        public string Description { get; set; }
        public decimal Starting_Bid { get; set; } = 1M;
        public List<string> Categories { get; set; }
        public List<ReturnBid> Bids { get; set; } 
    }
}