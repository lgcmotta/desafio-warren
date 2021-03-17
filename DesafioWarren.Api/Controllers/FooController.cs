using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioWarren.Api.Controllers
{
    [ApiController]
    [Route("api/v{versao:apiVersion}/[controller]")]
    [Authorize]
    [ApiVersion("1.0")]
    public class FooController : ControllerBase
    {
        [HttpGet]   
        public async Task<IActionResult> GetAsync()
        {
            return Ok();
        }
    }
}