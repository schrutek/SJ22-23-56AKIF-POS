using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Spg.SpengerShop.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // API - Design

        [HttpGet()]
        // https://localshost:7000/Products
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // https://localshost:7000/Products?filter=Awesome&SortOrder=Name_Desc&Status=12
        public IActionResult Get(string filter)
        {
            try
            {
                // TODO: Logik
                return Ok();
            }
            catch (KeyNotFoundException ex) // AuthenticationException
            {
                return Unauthorized();
            }
            catch (Exception ex) // ServiceLayerException
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        // https://localshost:7000/Products/{id}
        public IActionResult GetDetails(Guid id)
        {
            return Ok();
        }

        [HttpPost()]
        // https://localshost:7000/Products
        public IActionResult Create(NewProductDto dto)
        {
            return Ok();
        }

        [HttpPut()]
        // https://localshost:7000/Products/{id}
        public IActionResult Update(Guid id, NewProductDto dto)
        {
            return Ok();
        }

        [HttpDelete()]
        // https://localshost:7000/Products/{id}
        public IActionResult Update(Guid id)
        {
            return Ok();
        }

        public class NewProductDto
        {
        }
    }
}
