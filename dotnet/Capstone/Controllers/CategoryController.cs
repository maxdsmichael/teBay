using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("")]
    [ApiController]
    public class CategoryController : SilentAuctionController
    {
        private readonly ICategoryDAO cDao;

        public CategoryController(ICategoryDAO categoryDAO)
        {
            this.cDao = categoryDAO;
        }

        [HttpGet("Auction/Categories")]
        public IActionResult GetCategoriesForDisplay()
        {
            List<Category> categoryList = cDao.GetAllCategories();
            return Ok(categoryList);
        }
       
    }
}
