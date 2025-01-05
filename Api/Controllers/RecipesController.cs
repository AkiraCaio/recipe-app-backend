using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RecipeAppBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class RecipesController : ControllerBase
    { 
        [HttpGet("aaa")]
        public ActionResult<string> GetAAA() 
        { 
            return Ok("aaa");
            // return StatusCode(501, "aaaa");
        } 
    }   
}