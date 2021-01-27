using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SilentAuctionController : ControllerBase
    {
        public string UserId
        {
            get
            {                
                return User.Claims.Where(c => c.Type == "sub").FirstOrDefault()?.Value;
            }
        }

        public string Username
        {
            get
            {
                return User.Identity.Name;
            }
        }      
    }
}
