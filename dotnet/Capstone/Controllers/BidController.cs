using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers

{
    [Route("auction/item/[controller]")]
    [ApiController]
    [Authorize]
    public class BidController : ControllerBase
    {
        private readonly IBidDAO bDao;

        public BidController(IBidDAO bidDAO)
        {
            this.bDao = bidDAO;
        }

        [HttpGet("{id}")]
        public ActionResult<List<Bid>> GetBidsByItemID(int id)
        {
            List<Bid> bid = bDao.GetBidsByItem(id);
            return Ok(bid);
        }
        // [HttpGet(")]
        // /item/{item_id}/bids
    }
    
}
