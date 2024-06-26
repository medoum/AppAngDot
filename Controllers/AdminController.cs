using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudApi.Controllers
{
    [Authorize(Roles= "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("users")]
        public IEnumerable<string> Get()
        {
            return new List<string> { "Ahmed", "Ali", "Ahsan" };
        }
    }
}
