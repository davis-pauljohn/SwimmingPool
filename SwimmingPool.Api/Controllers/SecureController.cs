using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SwimmingPool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SecureController : ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return Ok(new int[] { 5, 4, 3, 2, 1 });
        }
    }
}