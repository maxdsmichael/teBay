using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;

namespace Capstone.Controllers
{
    [Route("")]
    [ApiController]
    
    public class ItemController : SilentAuctionController
    {
        //TODO: Create a Stored Procedure to add a CHECK constraint to the database that keep bids in the correct time
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
           var UserName = HttpContext.User.Identity.Name;
            return Ok(UserName);
        }

        private readonly IItemDAO itemDao;
        private readonly IBidDAO bidDao;

        public ItemController(IItemDAO itemDAO, IBidDAO bidDAO)
        {
            this.itemDao = itemDAO;
            this.bidDao = bidDAO;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    var UserName = HttpContext.User.Identity.Name;
        //    return Ok(UserName);
        //}

        [HttpGet("/items")] 
        public IActionResult GetAllItemsForDisplay()
        {
            List<Item> itemList = itemDao.GetAllItems();
            return Ok(itemList);
        }

        // Might want endpoint to add an item
        // POST '/items'
        [HttpPost("/items")] 
        [Authorize]
        public IActionResult AddItem(Item item)
        {
            IActionResult result;
            Item newItem = itemDao.AddNewItem(item);
            if (item != null)
            {
                result = Created(item.Title, null);
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and the item was not added." });
            }
            return result;
        }

        // Might want endpoint to update existing item
        // PUT '/items/{id}
        [HttpPut("/items/{itemID}")]
        [Authorize]
        public IActionResult UpdateItem(Item item)
        {
            return Ok();
        }


        // Might want endpoint that gets one item
        // GET '/items/{id}
        [HttpGet("/items/{itemID}")]
        public IActionResult GetSingleItem(int itemID)
        {
            try
            {
                Item item = itemDao.GetSingleItem(itemID);
                return Ok(item);
            }
            catch (Exception e)
            {
                return StatusCode(501, e);
            }
            
        }


        // Might want endpoint that returns all bids for an item
        // GET '/items/{id}/bids
        //[HttpGet("/items/{itemID}/bids")]
        //public IActionResult GetAllBids()
        //{
        //    return Ok();
        //}

        // Add new bid
        // POST '/items/{id}/bids
        [Authorize]
        [HttpPost("/items/{itemID}/bids")]
        [Authorize]
        public ActionResult<Bid> AddNewBid(int itemID, Bid bid)
        {
            if (UserId == null)
            {
                return Unauthorized();
            }

            bid.User_ID = int.Parse(UserId);
            decimal amountToCheck = bidDao.GetHighestBidAmountForItem(itemID) + 1m;
            if (bid.Amount >= amountToCheck )
            {
                ReturnBid returnedBid = bidDao.AddBid(bid);
                return Created("", returnedBid);
            }
          else
            {
                return BadRequest();
            }

            // Error check that item_id inside the bid object matches url item_id
            // User id will be pulled by authorized logged in user, not userid from json  
        }

        [HttpGet("/user/bids")]
        [Authorize]
        public ActionResult<List<Bid>> GetBidHistory()
        {
            List<Bid> bidHistory = bidDao.GetBidHistoryForUser(int.Parse(UserId));
            if (bidHistory != null && bidHistory.Count > 0)
            {
                return Ok(bidHistory);
            }
            else
            {
                // TODO: should a different code be sent back?
                return Unauthorized();
            }
        }

        [HttpGet("/update")]
        public ActionResult<List<Bid>> GetAllTopBids()
        {
            List<Bid> topBids = bidDao.GetHighestBidForAllItems();
            if (topBids != null && topBids.Count > 0)
            {
                return Ok(topBids);
            }
            else
            {
                // TODO: should a different code be sent back?
                return Unauthorized();
            }
        }

        [HttpGet("/user/bids/current")]
        [Authorize]
        public ActionResult<List<Bid>> GetBidHistoryPeritem()
        {
            List<Bid> currentBid = bidDao.GetHighestUserBidForEachItem(int.Parse(UserId));
            if (currentBid != null && currentBid.Count > 0)
            {
                return Ok(currentBid);
            }
            else
            {
                // TODO: should a different code be sent back?
                return Unauthorized();
            }
        }

        [HttpGet("/end-of-auction")]
        public ActionResult<List<Item>> GetEndOfAuction()
        {
            List<Item> winningItemsForUser;
            try
            {
                winningItemsForUser = itemDao.GetCurrentUserWinnings(int.Parse(UserId));
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
            return winningItemsForUser;
        }
    }
}